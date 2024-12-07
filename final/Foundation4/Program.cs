using System;
using System.Collections.Generic;

public abstract class Activity
{
    protected DateTime date;
    protected int minutes;

    public Activity(DateTime date, int minutes)
    {
        this.date = date;
        this.minutes = minutes;
    }

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    public string GetSummary()
    {
        return $"{date.ToString("dd MMM yyyy")} {this.GetType().Name} ({minutes} min): Distance {GetDistance():0.0} km, Speed: {GetSpeed():0.0} kph, Pace: {GetPace():0.0} min per km";
    }
}

public class Running : Activity
{
    private double distance; // in kilometers

    public Running(DateTime date, int minutes, double distance) : base(date, minutes)
    {
        this.distance = distance;
    }

    public override double GetDistance()
    {
        return distance;
    }

    public override double GetSpeed()
    {
        return (distance / minutes) * 60; // km/h
    }

    public override double GetPace()
    {
        return minutes / distance; // min/km
    }
}

public class Cycling : Activity
{
    private double speed; // kilometers per hour

    public Cycling(DateTime date, int minutes, double speed) : base(date, minutes)
    {
        this.speed = speed;
    }

    public override double GetDistance()
    {
        return (speed * minutes) / 60; // km
    }

    public override double GetSpeed()
    {
        return speed; // already in km/h
    }

    public override double GetPace()
    {
        return 60 / speed; // min/km
    }
}

public class Swimming : Activity
{
    private int laps; // number of laps

    public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        this.laps = laps;
    }

    public override double GetDistance()
    {
        return laps * 50 / 1000.0 * 0.62; // convert meters to kilometers
    }

    public override double GetSpeed()
    {
        return (GetDistance() / minutes) * 60; // km/h
    }

    public override double GetPace()
    {
        return minutes / GetDistance(); // min/km
    }
}

class Program
{
    static void Main()
    {
        // create a list of activities
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0), // running: 3 km in 30 minutes
            new Cycling(new DateTime(2022, 11, 3), 45, 20.0), // cycling: 20 km/h for 45 minutes
            new Swimming(new DateTime(2022, 11, 3), 30, 40)  // swimming: 40 laps in 30 minutes
        };

        // print the summary for each activity
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}