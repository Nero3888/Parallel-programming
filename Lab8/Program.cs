using System;
using System.Threading;

namespace RandomBitMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество строк матрицы:");
            int rows = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите количество столбцов матрицы:");
            int cols = int.Parse(Console.ReadLine());

            // Создаем объект для хранения результата
            MatrixData matrixData = new MatrixData(rows, cols);

            // Запускаем поток для генерации матрицы
            Thread matrixThread = new Thread(new ParameterizedThreadStart(GenerateRandomBitMatrix));
            matrixThread.Start(matrixData);

            // Ждем завершения потока
            matrixThread.Join();

            // Печатаем матрицу
            PrintMatrix(matrixData.Matrix);
        }

        static void GenerateRandomBitMatrix(object obj)
        {
            MatrixData matrixData = (MatrixData)obj;
            Random rand = new Random();

            for (int i = 0; i < matrixData.Rows; i++)
            {
                for (int j = 0; j < matrixData.Cols; j++)
                {
                    matrixData.Matrix[i, j] = rand.Next(2); // Генерирует 0 или 1
                }
            }
        }

        static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }

    // Класс для хранения данных матрицы
    class MatrixData
    {
        public int Rows { get; }
        public int Cols { get; }
        public int[,] Matrix { get; }

        public MatrixData(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Matrix = new int[rows, cols];
        }
    }
}