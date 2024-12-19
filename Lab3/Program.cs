using System;
using System.Threading;
using System.Threading.Tasks;

namespace StringSearchWithDelegates
{
    class Program
    {
        // Определение делегата, который принимает строку и символ для поиска
        public delegate bool CharSearchDelegate(string inputString, char searchChar);

        static async Task Main(string[] args)
        {
            // Инициализация делегата с использованием лямбда-выражения
            CharSearchDelegate searchDelegate = (inputString, searchChar) =>
            {
                // Проверка наличия символа в строке
                return inputString.Contains(searchChar);
            };

            // Ввод строки и символа пользователем
            Console.Write("Введите строку: ");
            string userInput = Console.ReadLine();
            Console.Write("Введите символ для поиска: ");
            char searchChar = Console.ReadKey().KeyChar;

            Console.WriteLine(); 

            // Создание CancellationTokenSource с тайм-аутом 5 секунд
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            try
            {
                // Запуск асинхронного метода с тайм-аутом
                bool result = await SearchWithTimeout(searchDelegate, userInput, searchChar, cts.Token);

                // Вывод результата
                if (result)
                {
                    Console.WriteLine($"Символ '{searchChar}' найден в строке: \"{userInput}\".");
                }
                else
                {
                    Console.WriteLine($"Символ '{searchChar}' не найден в строке: \"{userInput}\".");
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Операция поиска была отменена из-за превышения времени ожидания.");
            }
        }

        private static async Task<bool> SearchWithTimeout(CharSearchDelegate searchDelegate, string inputString, char searchChar, CancellationToken cancellationToken)
        {
            // Эмуляция длительной операции с выводом информации о ходе выполнения
            return await Task.Run(() =>
            {
                // Сообщение о начале поиска
                Console.WriteLine("Начинается поиск символа...");
                // Эмуляция задержки, чтобы показать ход выполнения
                for (int i = 0; i < 5; i++)
                {
                    // Проверка на отмену операции
                    cancellationToken.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                    Console.WriteLine($"Поиск продолжается... {i + 1}/5");
                }

                // Выполнение поиска
                return searchDelegate(inputString, searchChar);
            }, cancellationToken);
        }
    }
}
