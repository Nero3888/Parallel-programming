using System;
using System.Threading;

internal class Program
{
    // Метод для преобразования массива случайных чисел
    static void TransformArray()
    {
        // Получаем идентификатор текущего потока
        int threadID = Thread.CurrentThread.ManagedThreadId;
        Console.WriteLine("Поток {0}. Создание массива...", threadID);

        // Определяем размер массива
        int N = 10;
        int[] mas = new int[N];

        Console.WriteLine("Поток {0}. Инициализация элементов массива...", threadID);

        // Создаем объект Random для генерации случайных чисел
        Random rnd = new Random();

        // Заполняем массив случайными числами
        for (int i = 0; i < N; i++)
        {
            mas[i] = rnd.Next(1000);
        }

        // Выводим исходный массив в консоль
        Console.WriteLine("Поток {0}. Массив: [{1}]", threadID, string.Join(", ", mas));

        Console.WriteLine("Поток {0}. Преобразование массива...", threadID);

        // Преобразуем массив: заменяем элементы, кратные 3, на их квадрат
        for (int i = 0; i < mas.Length; i++)
        {
            if (mas[i] % 3 == 0)
            {
                mas[i] = mas[i] * mas[i]; 
            }
        }

        // Выводим преобразованный массив в консоль
        Console.WriteLine("Поток {0}. Преобразованный массив: [{1}]", threadID, string.Join(", ", mas));
    }

    // Главный метод программы
    static void Main(string[] args)
    {
        Action method = TransformArray; 
        Thread[] tmas = new Thread[20]; 

        // Запускаем 20 потоков
        for (int i = 0; i < 20; i++)
        {
            tmas[i] = new Thread(TransformArray); 
            tmas[i].Start(); 
        }

        Console.ReadKey();
    }
}