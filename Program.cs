using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

abstract class Person
{
    public abstract void DisplayDetails();
}

class Writer : Person
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Language { get; set; }
    public int BookCount { get; set; }

    public override void DisplayDetails()
    {
        Console.WriteLine("Прізвище: {0}", LastName);
        Console.WriteLine("Ім'я: {0}", FirstName);
        Console.WriteLine("Мова: {0}", Language);
        Console.WriteLine("Кількість книжок: {0}", BookCount);
    }
}

class Performance : Person
{
    public string Date { get; set; }
    public string Place { get; set; }
    public int AudienceCount { get; set; }

    public override void DisplayDetails()
    {
        Console.WriteLine("Дата виступу: {0}", Date);
        Console.WriteLine("Місце виступу: {0}", Place);
        Console.WriteLine("Кількість слухачів: {0}", AudienceCount);
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Person> people = new List<Person>();
        List<Performance> performances = new List<Performance>();

        while (true)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Додати запис про письменника");
            Console.WriteLine("2. Додати запис про виступ");
            Console.WriteLine("3. Відобразити всі записи");
            Console.WriteLine("4. Зберегти дані до файлу");
            Console.WriteLine("5. Вихід");

            Console.Write("Введіть ваш вибір: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Writer writer = ReadWriterFromConsole();
                    people.Add(writer);
                    break;
                case "2":
                    Performance performance = ReadPerformanceFromConsole();
                    performances.Add(performance);
                    break;
                case "3":
                    DisplayRecords(people, performances);
                    DisplayAdditionalInfo(people, performances);
                    break;
                case "4":
                    SaveDataToFile(people, performances);
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Некоректний вибір.");
                    break;
            }
        }
    }

    static void DisplayRecords(List<Person> people, List<Performance> performances)
    {
        Console.WriteLine("Записи про людей:");
        foreach (var person in people)
        {
            person.DisplayDetails();
        }
        Console.WriteLine();
        Console.WriteLine("Записи про виступи:");
        foreach (var performance in performances)
        {
            performance.DisplayDetails();
        }
        Console.WriteLine();
    }

    static void DisplayAdditionalInfo(List<Person> people, List<Performance> performances)
    {
        Console.WriteLine();

        // Сумарна кількість слухачів
        int totalAudienceCount = performances.Sum(p => p.AudienceCount);
        Console.WriteLine($"Сумарна кількість слухачів: {totalAudienceCount}");

        // День з найменшою кількістю слухачів
        var minAudienceDay = performances.OrderBy(p => p.AudienceCount).FirstOrDefault();
        if (minAudienceDay != null)
        {
            Console.WriteLine($"День з найменшою кількістю слухачів: {minAudienceDay.Date}");
        }
        else
        {
            Console.WriteLine("Немає даних про виступи.");
        }

        // Довжина прізвища письменників
        int maxLastNameLength = 0;
        foreach (var person in people)
        {
            if (person is Writer writer)
            {
                if (writer.LastName.Length > maxLastNameLength)
                {
                    maxLastNameLength = writer.LastName.Length;
                }
            }
        }
        Console.WriteLine($"Максимальна довжина прізвища письменників: {maxLastNameLength}");
    }

    static void SaveDataToFile(List<Person> people, List<Performance> performances)
    {
        string filePath = @"D:\Дз\Програмування\labb5.txt";

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Записи про людей:");
            foreach (var person in people)
            {
                writer.WriteLine(person.GetType().Name);
                writer.WriteLine(person.LastName);
                writer.WriteLine(person.FirstName);
                writer.WriteLine(person.Language);
                writer.WriteLine(person.BookCount);
            }
            writer.WriteLine();

            writer.WriteLine("Записи про виступи:");
            foreach (var performance in performances)
            {
                writer.WriteLine(performance.Date);
                writer.WriteLine(performance.Place);
                writer.WriteLine(performance.AudienceCount);
            }
        }

        Console.WriteLine($"Дані збережено до файлу: {filePath}");
    }

    static Writer ReadWriterFromConsole()
    {
        Writer writer = new Writer();

        writer.LastName = ReadStringFromConsole("Прізвище: ");
        writer.FirstName = ReadStringFromConsole("Ім'я: ");
        writer.Language = ReadStringFromConsole("Мова: ");
        writer.BookCount = ReadIntFromConsole("Кількість книжок: ");

        return writer;
    }

    static Performance ReadPerformanceFromConsole()
    {
        Performance performance = new Performance();

        performance.Date = ReadStringFromConsole("Дата виступу: ");
        performance.Place = ReadStringFromConsole("Місце виступу: ");
        performance.AudienceCount = ReadIntFromConsole("Кількість слухачів: ");

        return performance;
    }

    static string ReadStringFromConsole(string prompt)
    {
        string value;
        do
        {
            Console.Write(prompt);
            value = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Помилка: Введіть текст.");
            }
        } while (string.IsNullOrWhiteSpace(value));

        return value;
    }

    static int ReadIntFromConsole(string prompt)
    {
        int value;
        string input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine();
            if (!int.TryParse(input, out value))
            {
                Console.WriteLine("Помилка: Введіть ціле число.");
            }
        } while (!int.TryParse(input, out value));

        return value;
    }
}
