using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace FirstTelegramBot
{
    public class Processing
    {
        public static async Task<Message> RemoveKeyboard(ITelegramBotClient botClient, Message message)
        {
            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                        text: "Keyboard has been removed successully",
                                                        replyMarkup: new ReplyKeyboardRemove());
        }

        public static async Task<Message> GetRate(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Курс доллара", $"Dollar rate: 1$ = {RateParse.GetRate(TypesOfCurrencies.Dollar).Result} rubles."),
                    InlineKeyboardButton.WithCallbackData("Курс евро", $"Euro rate: 1€ = {RateParse.GetRate(TypesOfCurrencies.Euro).Result} rubles.")
                }
            });

            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                 text: "Choose",
                                                 replyMarkup: inlineKeyboard);
        }

        public static async Task<Message> Usage(ITelegramBotClient botClient, Message message)
        {
            const string usage = "Usage:\n" +
                                 "/remove - remove a keyboard\n" +
                                 "/currencyrate - get a currency rate\n";

            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                        text: usage,
                                                        replyMarkup: new ReplyKeyboardRemove());
        }
    }
}
