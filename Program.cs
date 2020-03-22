using System;

namespace DiscordBannerBot
{
    public class Program
    {
        static void Main(string[] args)
        {
            DiscordBot bot = new DiscordBot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
