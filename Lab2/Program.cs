using System;
using System.Threading.Tasks;

namespace StringSearchWithDelegates
{
    class Program
    {
        // Определение делегата, который принимает для поиска
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

            Console.WriteLine(); // Перенос строки

            // Запуск асинхронного метода
            bool result = await Task.Run(() =>
            {
                // Эмуляция длительной операции (например, поиск)
                return searchDelegate(userInput, searchChar);
            });

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
    }
}