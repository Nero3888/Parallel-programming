
using System;
using System.Threading.Tasks;

namespace EvenNumbersInMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = 4;
            int columns = 5;
            int[,] matrix = GenerateMatrix(rows, columns);
            DisplayMatrix(matrix);

            // Запускаем задачу для подсчета четных элементов
            Task<int> evenCountTask = Task.Run(() => CountEvenNumbers(matrix));

            // Ожидаем завершения задачи и получаем результат
            int evenCount = evenCountTask.Result;

            Console.WriteLine($"Количество четных элементов в матрице: {evenCount}");
            Console.ReadLine();
        }

        static int[,] GenerateMatrix(int rows, int columns)
        {
            Random rand = new Random();
            int[,] matrix = new int[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = rand.Next(1, 100); 
                }
            }
            return matrix;
        }

        static void DisplayMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            Console.WriteLine("Сгенерированная матрица:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        static int CountEvenNumbers(int[,] matrix)
        {
            int count = 0;
            int rows = matrix.GetLength(0); 
            int columns = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (matrix[i, j] % 2 == 0)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
