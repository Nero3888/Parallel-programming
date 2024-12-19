using System;
using System.Collections.Generic;
using System.Threading;

class SearchString
{
    public string Value { get; }

    public SearchString(string value)
    {
        Value = value;
    }
}

class StringCollection
{
    private List<SearchString> strings;

    public StringCollection()
    {
        strings = new List<SearchString>();
    }

    public void AddString(string str)
    {
        strings.Add(new SearchString(str));
    }

    public void SearchInStrings(char character, int delay)
    {
        foreach (var searchString in strings)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                bool found = SearchInString(searchString, character, delay);
                if (found)
                {
                    Console.WriteLine($"Symbol '{character}' found in string: '{searchString.Value}'");
                }
                else
                {
                    Console.WriteLine($"Symbol '{character}' not found in string: '{searchString.Value}'.");
                }
            });
        }

        // Задержка, чтобы дождаться завершения всех потоков
        Thread.Sleep(delay * strings.Count);
    }

    private bool SearchInString(SearchString searchString, char character, int delay)
    {
        Thread.Sleep(delay);  
        return searchString.Value.Contains(character);
    }
}

class MatrixOperations
{
    public int[,] GenerateRandomMatrix(out int rows, out int cols)
    {
        Random random = new Random();
        rows = random.Next(1, 11); 
        cols = random.Next(1, 11); 
        int[,] matrix = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = random.Next(1, 101); 
            }
        }

        return matrix;
    }

    public int SumEvenDivisibleByFour(int[,] matrix)
    {
        int sum = 0;
        foreach (var value in matrix)
        {
            if (value % 2 == 0 && value % 4 == 0)
            {
                sum += value;
            }
        }
        return sum;
    }
    public int FindMaxDivisibleByThree(int[,] matrix)
    {
        int max = int.MinValue;
        bool found = false;
        foreach (var value in matrix)
        {
            if (value % 3 == 0)
            {
                found = true;
                if (value > max)
                {
                    max = value;
                }
            }
        }
        return found ? max : -1; 
    }

    public int FindMinElement(int[,] matrix)
    {
        int min = int.MaxValue;
        foreach (var value in matrix)
        {
            if (value < min)
            {
                min = value;
            }
        }
        return min;
    }
}

class Program
{
    static void Main(string[] args)
    {
        StringCollection collection = new StringCollection();

        // Добавление строк в коллекцию
        collection.AddString("Hello, World!");
        collection.AddString("C# is great.");
        collection.AddString("Threading is useful.");
        collection.AddString("Searching for characters.");

        char charToSearch = 'o';

        // Задержка в миллисекундах
        int delay = 1000; 
        Console.WriteLine($"Searching for '{charToSearch}' with a delay of {delay} ms...");

        collection.SearchInStrings(charToSearch, delay);

        // Генерация матрицы случайных чисел
        MatrixOperations matrixOps = new MatrixOperations();
        int rows, cols;
        int[,] randomMatrix = matrixOps.GenerateRandomMatrix(out rows, out cols);

        Console.WriteLine("Generated matrix:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(randomMatrix[i, j] + "\t");
            }
            Console.WriteLine();
        }

        // Выполнение задач продолжения
        int sum = matrixOps.SumEvenDivisibleByFour(randomMatrix);
        Console.WriteLine($"Sum of even numbers divisible by 4: {sum}");

        int maxDivByThree = matrixOps.FindMaxDivisibleByThree(randomMatrix);
        if (maxDivByThree != -1)
        {
            Console.WriteLine($"Maximum number divisible by 3: {maxDivByThree}");
        }
        else
        {
            Console.WriteLine("No elements divisible by 3 found.");
        }

        int minElement = matrixOps.FindMinElement(randomMatrix);
        Console.WriteLine($"Minimum element in the matrix: {minElement}");

        // Задержка перед завершением программы
        Thread.Sleep(2000);
    }
}