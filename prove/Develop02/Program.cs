using System;
using System.Collections.Generic;
using System.IO;

namespace JournalApp
{
    // Represents a single journal entry
    class JournalEntry
    {
        public string Prompt { get; set; }
        public string Response { get; set; }
        public string Date { get; set; }

        public JournalEntry(string prompt, string response)
        {
            Prompt = prompt;
            Response = response;
            Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        // Method to display a journal entry
        public void DisplayEntry()
        {
            Console.WriteLine($"Date: {Date}");
            Console.WriteLine($"Prompt: {Prompt}");
            Console.WriteLine($"Response: {Response}\n");
        }

        // Method to format the journal entry for file saving
        public string FormatForSaving()
        {
            return $"{Date} | {Prompt} | {Response}";
        }

        // Method to parse an entry from a file
        public static JournalEntry ParseEntry(string entryLine)
        {
            string[] parts = entryLine.Split('|');
            if (parts.Length >= 3)
            {
                return new JournalEntry(parts[1].Trim(), parts[2].Trim())
                {
                    Date = parts[0].Trim()
                };
            }
            return null;
        }
    }

    // Manages a collection of journal entries
    class Journal
    {
        private List<JournalEntry> entries;
        private List<string> prompts = new List<string>
        {
            "What are some miracles you had today?",
            "Tell me what you did today!",
            "Something interesting happened?",
            "What are some of the bad things that happened to you today?",
            "What are some of the good things that happened to you today?"
        };

        public Journal()
        {
            entries = new List<JournalEntry>();
        }

        // Adds a new entry with a random prompt
        public void AddEntry()
        {
            Random random = new Random();
            string randomPrompt = prompts[random.Next(prompts.Count)];
            Console.WriteLine($"Prompt: {randomPrompt}");
            Console.Write("Response: ");
            string response = Console.ReadLine();

            JournalEntry newEntry = new JournalEntry(randomPrompt, response);
            entries.Add(newEntry);
            Console.WriteLine("Entry added successfully.\n");
        }

        // Displays all journal entries
        public void DisplayEntries()
        {
            if (entries.Count == 0)
            {
                Console.WriteLine("No journal entries found.\n");
            }
            else
            {
                Console.WriteLine("Journal Entries:\n");
                foreach (var entry in entries)
                {
                    entry.DisplayEntry();
                }
            }
        }

        // Saves journal entries to a file
        public void SaveToFile(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var entry in entries)
                {
                    writer.WriteLine(entry.FormatForSaving());
                }
            }
            Console.WriteLine("Journal saved to file successfully.\n");
        }

        // Loads journal entries from a file
        public void LoadFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                entries.Clear(); // Clear the current entries before loading
                string[] lines = File.ReadAllLines(fileName);
                foreach (var line in lines)
                {
                    JournalEntry entry = JournalEntry.ParseEntry(line);
                    if (entry != null)
                    {
                        entries.Add(entry);
                    }
                }
                Console.WriteLine("Journal loaded from file successfully.\n");
            }
            else
            {
                Console.WriteLine("File not found.\n");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal();
            while (true)
            {
                Console.WriteLine("Journal Menu:");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display journal entries");
                Console.WriteLine("3. Save journal to a file");
                Console.WriteLine("4. Load journal from a file");
                Console.WriteLine("5. Exit");
                Console.Write("Are you ready? Choose an option!:  ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        journal.AddEntry();
                        break;
                    case "2":
                        journal.DisplayEntries();
                        break;
                    case "3":
                        Console.Write("Enter filename to save: ");
                        string saveFile = Console.ReadLine();
                        journal.SaveToFile(saveFile);
                        break;
                    case "4":
                        Console.Write("Enter filename to load: ");
                        string loadFile = Console.ReadLine();
                        journal.LoadFromFile(loadFile);
                        break;
                    case "5":
                        Console.WriteLine("Exiting the journal application. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.\n");
                        break;
                }
            }
        }
    }
}
