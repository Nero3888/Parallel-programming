using System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        // Генерация случайного размера матрицы
        Random random = new Random();
        int rows = random.Next(1, 11); 
        int cols = random.Next(1, 11); 

        // Генерация матрицы случайных чисел
        int[,] matrix = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = random.Next(1, 101); 
            }
        }

        // Вывод сгенерированной матрицы
        Console.WriteLine("Сгенерированная матрица:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }

        // Создание задач продолжения
        var sumTask = Task.Run(() => CalculateSumOfEvenDivisibleByFour(matrix));
        var maxTask = Task.Run(() => FindMaxDivisibleByThree(matrix));
        var minTask = Task.Run(() => FindMinElement(matrix));

        // Ожидание завершения всех задач
        Task.WhenAll(sumTask, maxTask, minTask).ContinueWith(t =>
        {
            // Вывод результатов
            Console.WriteLine($"Сумма четных элементов, делящихся на 4: {sumTask.Result}");
            Console.WriteLine($"Максимальный элемент, делящийся на 3: {maxTask.Result}");
            Console.WriteLine($"Минимальный элемент: {minTask.Result}");
        }).Wait();
    }

    static int CalculateSumOfEvenDivisibleByFour(int[,] matrix)
    {
        int sum = 0;
        foreach (var item in matrix)
        {
            if (item % 2 == 0 && item % 4 == 0)
            {
                sum += item;
            }
        }
        return sum;
    }

    static int FindMaxDivisibleByThree(int[,] matrix)
    {
        int max = int.MinValue;
        bool found = false;
        foreach (var item in matrix)
        {
            if (item % 3 == 0)
            {
                found = true;
                if (item > max)
                {
                    max = item;
                }
            }
        }
        return found ? max : -1; 
    }

    static int FindMinElement(int[,] matrix)
    {
        int min = int.MaxValue;
        foreach (var item in matrix)
        {
            if (item < min)
            {
                min = item;
            }
        }
        return min;
    }
}