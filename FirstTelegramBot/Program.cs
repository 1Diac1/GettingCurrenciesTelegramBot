using Telegram.Bot.Extensions.Polling;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot;
using System;

namespace FirstTelegramBot
{
    class Program
    {
        private static readonly string token = "your_token";
        private static ITelegramBotClient botClient;

        static async Task Main(string[] args)
        {
            try
            {
                botClient = new TelegramBotClient(token);

                var me = await botClient.GetMeAsync();

                using var cts = new CancellationTokenSource();

                // start receiving
                botClient.StartReceiving(new DefaultUpdateHandler(Handlers.HandleUpdateAsync, Handlers.HandleErrorAsync), cts.Token);

                Console.WriteLine($"Telegram bot has been started successully for { me.Username }");

                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
            }
            catch (Exception exception) { Console.WriteLine(exception.Message); }
        }
    }
}
