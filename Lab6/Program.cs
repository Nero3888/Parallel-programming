using System;
using System.Threading;

internal class Program
{
    // Массив для хранения данных
    static int[] data;
    // Переменные для хранения суммы, среднего и количества элементов, кратных 3
    static int sum = 0;
    static double average = 0;
    static int countDivBy3 = 0;
    // Объект для синхронизации потоков
    static readonly object lockObject = new object();

    // Метод для вычисления статистики в заданном диапазоне
    static void CalculateStatistics(int start, int end)
    {
        int localSum = 0; 
        int localCountDivBy3 = 0; 

        // Проходим по элементам массива в заданном диапазоне
        for (int i = start; i < end; i++)
        {
            localSum += data[i];
            if (data[i] % 3 == 0)
            {
                localCountDivBy3++;
            }
        }

        // Блокируем доступ к общим переменным
        lock (lockObject)
        {
            sum += localSum; 
            countDivBy3 += localCountDivBy3; 
        }
    }

    // Главный метод программы
    static void Main(string[] args)
    {
        int N = 1000000; 
        data = new int[N]; 
        Random rnd = new Random(); 

        // Заполнение массива случайными числами от 0 до 999
        for (int i = 0; i < N; i++)
        {
            data[i] = rnd.Next(1000);
        }

        int numberOfThreads = 4;
        Thread[] threads = new Thread[numberOfThreads]; 
        int chunkSize = N / numberOfThreads; 

        // Запуск потоков
        for (int i = 0; i < numberOfThreads; i++)
        {
            int start = i * chunkSize; 
            int end = (i == numberOfThreads - 1) ? N : start + chunkSize; 
            threads[i] = new Thread(() => CalculateStatistics(start, end)); 
            threads[i].Start(); 
        }

        // Ожидание завершения всех потоков
        foreach (var thread in threads)
        {
            thread.Join(); 
        }

        // Вычисление среднего значения
        average = (double)sum / N;

        // Вывод результатов
        Console.WriteLine($"Сумма: {sum}");
        Console.WriteLine($"Среднее: {average}");
        Console.WriteLine($"Количество элементов, кратных 3: {countDivBy3}");
    }
}

