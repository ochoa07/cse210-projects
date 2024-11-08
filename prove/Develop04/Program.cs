using System;

public abstract class MindfulnessActivity
{
    protected int Duration;

    public MindfulnessActivity(int duration)
    {
        Duration = duration;
    }

    public void StartActivity(string activityName, string description)
    {
        Console.WriteLine($"\nStarting {activityName}...");
        Console.WriteLine(description);
        Console.WriteLine("Prepare to begin...");
        ShowPauseAnimation(3);
    }

    public void EndActivity(string activityName)
    {
        Console.WriteLine("Good job! You've completed the activity.");
        Console.WriteLine($"Activity: {activityName} Duration: {Duration} seconds");
        ShowPauseAnimation(3);
    }

    protected void ShowPauseAnimation(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"\rPausing... {i} ");
            System.Threading.Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    public abstract void RunActivity();
}

public class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity(int duration) : base(duration) { }

    public override void RunActivity()
    {
        StartActivity("Breathing Activity", 
            "This activity will help you relax by guiding your breathing. Focus on each breath.");

        int timePassed = 0;
        while (timePassed < Duration)
        {
            Console.WriteLine("Breathe in...");
            ShowPauseAnimation(2);
            Console.WriteLine("Breathe out...");
            ShowPauseAnimation(2);
            timePassed += 4;
        }

        EndActivity("Breathing Activity");
    }
}

public class ReflectionActivity : MindfulnessActivity
{
    private static readonly string[] Prompts = 
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something selfless."
    };

    private static readonly string[] Questions =
    {
        "Why was this meaningful to you?",
        "Have you ever done this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different?",
        "What could you learn from this experience?",
        "What did you learn about yourself?",
        "How can you use this experience in the future?"
    };

    public ReflectionActivity(int duration) : base(duration) { }

    public override void RunActivity()
    {
        StartActivity("Reflection Activity", 
            "Reflect on times when you showed strength. Recognize the power in yourself.");

        Console.WriteLine(Prompts[new Random().Next(Prompts.Length)]);
        int timePassed = 0;

        while (timePassed < Duration)
        {
            Console.WriteLine(Questions[new Random().Next(Questions.Length)]);
            ShowPauseAnimation(4);
            timePassed += 4;
        }

        EndActivity("Reflection Activity");
    }
}

public class ListingActivity : MindfulnessActivity
{
    private static readonly string[] Prompts = 
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who have you helped this week?",
        "When have you felt peace this month?",
        "Who are your personal heroes?"
    };

    public ListingActivity(int duration) : base(duration) { }

    public override void RunActivity()
    {
        StartActivity("Listing Activity", 
            "Reflect on the good things by listing as many as you can in a certain area.");

        Console.WriteLine(Prompts[new Random().Next(Prompts.Length)]);
        Console.WriteLine("Start listing items...");

        int itemCounter = 0;
        int timePassed = 0;
        
        while (timePassed < Duration)
        {
            Console.Write("Enter item: ");
            Console.ReadLine();
            itemCounter++;
            timePassed += 3;
        }

        Console.WriteLine($"You listed {itemCounter} items.");
        EndActivity("Listing Activity");
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nMindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 0) break;

            Console.Write("Enter duration in seconds: ");
            int duration = int.Parse(Console.ReadLine());

            MindfulnessActivity activity = choice switch
            {
                1 => new BreathingActivity(duration),
                2 => new ReflectionActivity(duration),
                3 => new ListingActivity(duration),
                _ => null
            };

            activity?.RunActivity();
        }
    }
}
