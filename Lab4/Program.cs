using System;

namespace StringEncryption
{
    // Объявление делегата
    public delegate void EncryptionCompleted(string encryptedString);

    class Program
    {
        static void Main(string[] args)
        {
            // Входные параметры
            string inputString = "Hello, World!";
            int shift = 3;

            // Создаем лямбда-выражение для обратного вызова
            EncryptionCompleted callback = (result) =>
            {
                Console.WriteLine("Зашифрованная строка: " + result);
            };

            // Вызов асинхронного метода шифрования
            EncryptStringAsync(inputString, shift, callback);

            // Простой способ подождать завершения асинхронной операции
            Console.WriteLine("Ожидание завершения шифрования...");
            Console.ReadLine(); 
        }

        // Асинхронный метод шифрования
        static async void EncryptStringAsync(string input, int shift, EncryptionCompleted callback)
        {
            // Симуляция длительной операции с помощью Task.Delay
            await System.Threading.Tasks.Task.Delay(2000);

            // Шифрование строки
            string encrypted = Encrypt(input, shift);

            // Вызов обратного вызова
            callback(encrypted);
        }

        // Метод для шифрования строки
        static string Encrypt(string input, int shift)
        {
            char[] buffer = input.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                // Шифрование каждого символа
                char letter = buffer[i];
                letter = (char)(letter + shift);
                buffer[i] = letter;
            }
            return new string(buffer);
        }
    }
}