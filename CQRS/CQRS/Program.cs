using System;
using System.Collections.Generic;

// Komut nesnesi
public class AddNoteCommand
{
    public string Title { get; set; }
    public string Body { get; set; }
}

// Komut işleyicisi
public class AddNoteCommandHandler
{
    private readonly List<string> _notes;

    public AddNoteCommandHandler(List<string> notes)
    {
        _notes = notes;
    }

    public void Handle(AddNoteCommand command)
    {
        _notes.Add($"{command.Title}: {command.Body}");
    }
}

// Sorgu nesnesi
public class GetNotesQuery
{
    public List<string> Notes { get; set; }
}

// Sorgu işleyicisi
public class GetNotesQueryHandler
{
    private readonly List<string> _notes;

    public GetNotesQueryHandler(List<string> notes)
    {
        _notes = notes;
    }

    public List<string> Handle()
    {
        return _notes;
    }
}

public class Program
{
    private static readonly List<string> Notes = new List<string>();
    private static readonly AddNoteCommandHandler AddNoteCommandHandler = new AddNoteCommandHandler(Notes);
    private static readonly GetNotesQueryHandler GetNotesQueryHandler = new GetNotesQueryHandler(Notes);

    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("1. Add note");
            Console.WriteLine("2. View notes");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter note title: ");
                    var title = Console.ReadLine();

                    Console.Write("Enter note body: ");
                    var body = Console.ReadLine();

                    AddNoteCommandHandler.Handle(new AddNoteCommand { Title = title, Body = body });
                    Console.WriteLine("Note added successfully.");
                    break;

                case "2":
                    var notes = GetNotesQueryHandler.Handle();

                    if (notes.Count == 0)
                    {
                        Console.WriteLine("No notes found.");
                    }
                    else
                    {
                        Console.WriteLine("Notes:");
                        foreach (var note in notes)
                        {
                            Console.WriteLine($"- {note}");
                        }
                    }
                    break;

                case "3":
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            Console.WriteLine();
        }
    }
}
