using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Points { get; private set; }
    public bool IsCompleted { get; set; }

    protected Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
        IsCompleted = false;
    }

    public abstract void RecordEvent();
    public abstract string GetStatus();

    public override string ToString()
    {
        return $"{Name} - {Description}";
    }
}

class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override void RecordEvent()
    {
        if (!IsCompleted)
        {
            IsCompleted = true;
            Console.WriteLine($"Completed: {Name}! You earned {Points} points.");
        }
        else
        {
            Console.WriteLine($"{Name} is already completed!");
        }
    }

    public override string GetStatus()
    {
        return IsCompleted ? "[X]" : "[ ]";
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override void RecordEvent()
    {
        Console.WriteLine($"Recorded: {Name}! You earned {Points} points.");
    }

    public override string GetStatus()
    {
        return "[∞]";
    }
}

class ChecklistGoal : Goal
{
    public int TargetCount { get; private set; }
    public int CurrentCount { get; private set; }
    public int BonusPoints { get; private set; }

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints)
        : base(name, description, points)
    {
        TargetCount = targetCount;
        CurrentCount = 0;
        BonusPoints = bonusPoints;
    }

    public override void RecordEvent()
    {
        if (CurrentCount < TargetCount)
        {
            CurrentCount++;
            Console.WriteLine($"Recorded: {Name}! You earned {Points} points.");
            if (CurrentCount == TargetCount)
            {
                IsCompleted = true;
                Console.WriteLine($"Goal Completed: {Name}! Bonus {BonusPoints} points!");
            }
        }
        else
        {
            Console.WriteLine($"{Name} is already completed!");
        }
    }

    public override string GetStatus()
    {
        return IsCompleted ? "[X]" : $"[ ] ({CurrentCount}/{TargetCount})";
    }
}

class EternalQuest
{
    private static List<Goal> Goals = new List<Goal>();
    private static int TotalScore = 0;

    static void Main()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("Eternal Quest Program");
            Console.WriteLine("1. View Goals");
            Console.WriteLine("2. Add Goal");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. View Score");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Load Goals");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewGoals();
                    break;
                case "2":
                    AddGoal();
                    break;
                case "3":
                    RecordEvent();
                    break;
                case "4":
                    ViewScore();
                    break;
                case "5":
                    SaveGoals();
                    break;
                case "6":
                    LoadGoals();
                    break;
                case "7":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice! Press Enter to try again.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    static void ViewGoals()
    {
        Console.Clear();
        Console.WriteLine("Goals:");
        if (Goals.Count == 0)
        {
            Console.WriteLine("No goals added yet!");
        }
        else
        {
            foreach (var goal in Goals)
            {
                Console.WriteLine($"{goal.GetStatus()} {goal}");
            }
        }
        Console.WriteLine("Press Enter to return to the menu.");
        Console.ReadLine();
    }

    static void AddGoal()
    {
        Console.Clear();
        Console.WriteLine("Add a Goal");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Choose a type of goal: ");
        string choice = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();
        Console.Write("Enter goal points: ");
        int points = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case "1":
                Goals.Add(new SimpleGoal(name, description, points));
                break;
            case "2":
                Goals.Add(new EternalGoal(name, description, points));
                break;
            case "3":
                Console.Write("Enter target count: ");
                int targetCount = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus points: ");
                int bonusPoints = int.Parse(Console.ReadLine());
                Goals.Add(new ChecklistGoal(name, description, points, targetCount, bonusPoints));
                break;
            default:
                Console.WriteLine("Invalid choice! Press Enter to return to the menu.");
                Console.ReadLine();
                return;
        }
        Console.WriteLine("Goal added! Press Enter to return to the menu.");
        Console.ReadLine();
    }

    static void RecordEvent()
    {
        Console.Clear();
        Console.WriteLine("Record an Event:");
        for (int i = 0; i < Goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Goals[i]}");
        }
        Console.Write("Choose a goal: ");
        int choice = int.Parse(Console.ReadLine()) - 1;

        if (choice >= 0 && choice < Goals.Count)
        {
            Goals[choice].RecordEvent();
            TotalScore += Goals[choice].Points;
        }
        else
        {
            Console.WriteLine("Invalid choice!");
        }
        Console.WriteLine("Press Enter to return to the menu.");
        Console.ReadLine();
    }

    static void ViewScore()
    {
        Console.Clear();
        Console.WriteLine($"Total Score: {TotalScore}");
        Console.WriteLine("Press Enter to return to the menu.");
        Console.ReadLine();
    }

    static void SaveGoals()
    {
        Console.Clear();
        Console.Write("Enter filename to save goals: ");
        string filename = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(TotalScore);
            foreach (var goal in Goals)
            {
                if (goal is SimpleGoal)
                {
                    writer.WriteLine($"Simple|{goal.Name}|{goal.Description}|{goal.Points}|{goal.IsCompleted}");
                }
                else if (goal is EternalGoal)
                {
                    writer.WriteLine($"Eternal|{goal.Name}|{goal.Description}|{goal.Points}");
                }
                else if (goal is ChecklistGoal checklistGoal)
                {
                    writer.WriteLine($"Checklist|{checklistGoal.Name}|{checklistGoal.Description}|{checklistGoal.Points}|{checklistGoal.TargetCount}|{checklistGoal.CurrentCount}|{checklistGoal.BonusPoints}|{checklistGoal.IsCompleted}");
                }
            }
        }
        Console.WriteLine("Goals saved successfully! Press Enter to return to the menu.");
        Console.ReadLine();
    }

    static void LoadGoals()
    {
        Console.Clear();
        Console.Write("Enter filename to load goals: ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found! Press Enter to return to the menu.");
            Console.ReadLine();
            return;
        }

        using (StreamReader reader = new StreamReader(filename))
        {
            TotalScore = int.Parse(reader.ReadLine());
            Goals.Clear();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split('|');
                string type = parts[0];
                string name = parts[1];
                string description = parts[2];
                int points = int.Parse(parts[3]);

                switch (type)
                {
                    case "Simple":
                        bool isCompleted = bool.Parse(parts[4]);
                        Goals.Add(new SimpleGoal(name, description, points) { IsCompleted = isCompleted });
                        break;
                    case "Eternal":
                        Goals.Add(new EternalGoal(name, description, points));
                        break;
                    case "Checklist":
                        int targetCount = int.Parse(parts[4]);
                        int currentCount = int.Parse(parts[5]);
                        int bonusPoints = int.Parse(parts[6]);
                        isCompleted = bool.Parse(parts[7]);
                        Goals.Add(new ChecklistGoal(name, description, points, targetCount, bonusPoints)
                        {
                            IsCompleted = isCompleted
                        });
                        break;
                }
            }
        }
        Console.WriteLine("Goals loaded successfully! Press Enter to return to the menu.");
        Console.ReadLine();
    }
}