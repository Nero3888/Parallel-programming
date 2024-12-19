using System;

class Program
{
    static void Main(string[] args)
    {
        int[] numbers = GenerateRandomArray(1000000);

        // Время выполнения без параллелизма
        var watch = System.Diagnostics.Stopwatch.StartNew();
        double oddPercentage = CalculateOddPercentage(numbers);
        watch.Stop();

        Console.WriteLine($"Процент нечетных элементов (без параллелизма): {oddPercentage}%");
        Console.WriteLine($"Время выполнения (без параллелизма): {watch.ElapsedMilliseconds} мс");

        // Время выполнения с использованием параллелизма
        watch.Restart();
        double oddPercentageParallel = CalculateOddPercentageParallel(numbers);
        watch.Stop();

        Console.WriteLine($"Процент нечетных элементов (с параллелизмом): {oddPercentageParallel}%");
        Console.WriteLine($"Время выполнения (с параллелизмом): {watch.ElapsedMilliseconds} мс");
    }

    static int[] GenerateRandomArray(int size)
    {
        Random random = new Random();
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = random.Next(1, 100);
        }
        return array;
    }

    static double CalculateOddPercentage(int[] array)
    {
        int oddCount = 0;
        foreach (var number in array)
        {
            if (number % 2 != 0)
            {
                oddCount++;
            }
        }
        return (double)oddCount / array.Length * 100;
    }

    static double CalculateOddPercentageParallel(int[] array)
    {
        int oddCount = 0;
        object lockObject = new object();

        System.Threading.Tasks.Parallel.ForEach(array, number =>
        {
            if (number % 2 != 0)
            {
                lock (lockObject)
                {
                    oddCount++;
                }
            }
        }); 
return (double)oddCount / array.Length * 100;
    }
}