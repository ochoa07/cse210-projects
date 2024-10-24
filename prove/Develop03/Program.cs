using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    // Represents a single word in the scripture
    public class Word
    {
        public string Text { get; private set; }
        public bool IsHidden { get; private set; }

        public Word(string text)
        {
            Text = text;
            IsHidden = false;
        }

        public void Hide()
        {
            IsHidden = true;
        }
    }

    // Represents the reference of the scripture (e.g., "John 3:16" or "Proverbs 3:5-6")
    public class Reference
    {
        public string VerseReference { get; private set; }

        // Constructor for single verse
        public Reference(string reference)
        {
            VerseReference = reference;
        }

        // Constructor for verse range
        public Reference(string book, int startChapter, int startVerse, int endVerse)
        {
            VerseReference = $"{book} {startChapter}:{startVerse}-{endVerse}";
        }

        public override string ToString()
        {
            return VerseReference;
        }
    }

    // Represents the scripture itself
    public class Scripture
    {
        public Reference ScriptureReference { get; private set; }
        private List<Word> Words { get; set; }

        public Scripture(Reference reference, string text)
        {
            ScriptureReference = reference;
            Words = text.Split(' ').Select(word => new Word(word)).ToList();
        }

        public string GetDisplayText()
        {
            return string.Join(" ", Words.Select(w => w.IsHidden ? "_____" : w.Text));
        }

        public void HideRandomWord(Random random)
        {
            var visibleWords = Words.Where(w => !w.IsHidden).ToList();
            if (visibleWords.Count > 0)
            {
                int index = random.Next(visibleWords.Count);
                visibleWords[index].Hide();
            }
        }

        public bool AllWordsHidden()
        {
            return Words.All(w => w.IsHidden);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Scripture Memorizer!");
            Console.WriteLine("Enter the reference (e.g., John 3:16):");
            string referenceInput = Console.ReadLine();

            Console.WriteLine("Enter the scripture text:");
            string scriptureText = Console.ReadLine();

            // Create reference and scripture objects
            Reference reference = new Reference(referenceInput);
            Scripture scripture = new Scripture(reference, scriptureText);

            Random random = new Random();

            while (true)
            {
                // Clear the console and display the complete scripture
                Console.Clear();
                Console.WriteLine($"{scripture.ScriptureReference}: {scripture.GetDisplayText()}");
                Console.WriteLine("\nPress Enter to hide a word or type 'quit' to exit.");

                string userInput = Console.ReadLine();
                if (userInput?.ToLower() == "quit")
                {
                    break; // Exit the program
                }

                // Hide a random word
                scripture.HideRandomWord(random);

                // Check if all words are hidden
                if (scripture.AllWordsHidden())
                {
                    Console.Clear();
                    Console.WriteLine("Congratulations! You've memorized the scripture!");
                    break;
                }
            }
        }
    }
}