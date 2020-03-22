using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DiscordBannerBot.BotCommands;

using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;


namespace DiscordBannerBot
{
    public class DiscordBot
    {
        #region Private Properties
        #endregion

        #region Public Properties
        public DiscordClient Client { get; private set; }
        public CommandsNextModule Commands { get; private set; }
        #endregion

        #region Constructors
        #endregion
        
        #region Private Methods
        private Task OnBotReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
        #endregion

        #region Public Methods
        public async Task RunAsync()
        {
            string json = String.Empty;

            using (var fs = File.OpenRead("config.json"))
            {
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                {
                    json = await sr.ReadToEndAsync().ConfigureAwait(false);
                }
            }

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration()
            {
                Token = configJson.Token, 
                TokenType = TokenType.Bot, 
                AutoReconnect = true, 
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            Client = new DiscordClient(config);
            Client.Ready += OnBotReady;

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefix = configJson.Prefix,
                EnableMentionPrefix = false,
                EnableDms = false, 
                EnableDefaultHelp = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<BannerCommand>();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }
        #endregion

    }
}
