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
        Thread.Sleep(delay);  // Искусственная задержка
        return searchString.Value.Contains(character);
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
        int delay = 1000; // Измените значение для тестирования
        Console.WriteLine($"Searching for '{charToSearch}' with a delay of {delay} ms...");

        collection.SearchInStrings(charToSearch, delay);

        // Задержка перед завершением программы
        Thread.Sleep(2000);
    }
}