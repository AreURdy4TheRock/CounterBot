using Telegram.Bot;
using Telegram.Bot.Types;
using CounterBot.Configuration;
using CounterBot.Services;
using Telegram.Bot.Types.Enums;

namespace CounterBot.Controllers
{
    public class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).Choice = callbackQuery.Data;

            // Генерим информационное сообщение
            string ChoiceText = callbackQuery.Data switch
            {
                "Длина" => " Длина строки",
                "Сумма" => " Сумма цифр",
                _ => String.Empty
            };

            //Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Выбран вариант - {ChoiceText}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}

