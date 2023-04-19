using Telegram.Bot;
using Telegram.Bot.Types;
using CounterBot.Configuration;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using CounterBot.Services;
using System.Threading;
using CounterBot.Extensions;

namespace CounterBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Длина строки" , $"Длина"),
                        InlineKeyboardButton.WithCallbackData($" Сумма цифр" , $"Сумма")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот считает длину строки или сумму чисел.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Хочешь узнать сколько символов в предложении? Хочешь посчитать цифры в строке? Тогда тебе к нам!{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;

                default:
                    switch (_memoryStorage.GetSession(message.Chat.Id).Choice) {

                        case "Длина":
                            await _telegramClient.SendTextMessageAsync(message.From.Id, $"Длина сообщения: {message.Text.Length} знаков", cancellationToken: ct);
                            break;
                        case "Сумма":
                            await _telegramClient.SendTextMessageAsync(message.From.Id, $"Сумма чисел: {SumNumbers.Sum(message.Text)}", cancellationToken: ct);
                            break;

                    }

                    break;
            }
        }
    }
}
