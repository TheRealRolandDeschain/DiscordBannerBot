using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace DiscordBannerBot.BotCommands
{
    public class BannerCommand
    {
        [Command("banner")]
        public async Task SendBanner(CommandContext ctx)
        {
            BannerCreator banner = new BannerCreator();
            string input = ctx.RawArgumentString;
            if(char.IsWhiteSpace(input, 0))
            {
                input = input.Remove(0, 1);
            }


            string reply = banner.CreateBannerMessage(input);
            await ctx.Channel.SendMessageAsync(@"```" + reply + @"```").ConfigureAwait(false);
        }
    }
}
