using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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

    public async Task SearchInStringsAsync(char character, int delay)
    {
        List<Task> tasks = new List<Task>();

        foreach (var searchString in strings)
        {
            tasks.Add(Task.Run(async () =>
            {
                bool found = await SearchInStringAsync(searchString, character, delay);
                if (found)
                {
                    Console.WriteLine($"Symbol '{character}' found in string: '{searchString.Value}'");
                }
                else
                {
                    Console.WriteLine($"Symbol '{character}' not found in string: '{searchString.Value}'.");
                }
            }));
        }

        await Task.WhenAll(tasks);
    }

    private async Task<bool> SearchInStringAsync(SearchString searchString, char character, int delay)
    {
        await Task.Delay(delay);  // Искусственная задержка
        return searchString.Value.Contains(character);
    }
}

class Program
{
    static async Task Main(string[] args)
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

        await collection.SearchInStringsAsync(charToSearch, delay);

        Console.WriteLine("Search completed.");
    }
}