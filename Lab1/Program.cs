using System;
using System.Collections.Generic;

namespace DelegateExample
{
    class Program
    {
        // Определение делегата
        public delegate void ActionWithFunc(Func<int> func, List<float> list);

        static void Main(string[] args)
        {
            // Инициализация делегата
            ActionWithFunc action = ExecuteAction;

            // Создание списка float
            List<float> floatList = new List<float> { 1.5f, 2.5f, 3.5f };

            // Вызов методов через делегат
            action(GetRandomNumber, floatList);
            action(GetSumOfList, floatList);
        }

        // Метод, возвращающий случайное число
        public static int GetRandomNumber()
        {
            Random random = new Random();
            return random.Next(1, 100); 
        }

        // Метод, возвращающий сумму элементов списка
        public static int GetSumOfList()
        {
            return 0; // Заглушка для совместимости с Func<int>
        }

        // Метод, который принимает делегат Func<int> и список float
        public static void ExecuteAction(Func<int> func, List<float> list)
        {
            // Вызов функции и вывод результата
            int result = func();
            Console.WriteLine($"Результат вызова функции: {result}");

            // Пример работы со списком
            Console.WriteLine("Элементы списка:");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}