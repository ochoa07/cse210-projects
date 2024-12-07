using System;
using System.Collections.Generic;

// address class
public class Address
{
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public bool IsInUSA()
    {
        return country.ToUpper() == "USA";
    }

    public string GetFullAddress()
    {
        return $"{street}\n{city}, {state}\n{country}";
    }
}

// event (base class)
public class Event
{
    private string title;
    private string description;
    private string date;
    private string time;
    private Address address;

    public Event(string title, string description, string date, string time, Address address)
    {
        this.title = title;
        this.description = description;
        this.date = date;
        this.time = time;
        this.address = address;
    }

    public virtual string GetStandardDetails()
    {
        return $"Event: {title}\nDescription: {description}\nDate: {date}\nTime: {time}\nAddress: \n{address.GetFullAddress()}";
    }

    public virtual string GetFullDetails()
    {
        return GetStandardDetails();
    }

    public virtual string GetShortDescription()
    {
        return $"{GetType().Name} - {title} ({date})";
    }
}

// lecture class (derived from event)
public class Lecture : Event
{
    private string speaker;
    private int capacity;

    public Lecture(string title, string description, string date, string time, Address address, string speaker, int capacity)
        : base(title, description, date, time, address)
    {
        this.speaker = speaker;
        this.capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nSpeaker: {speaker}\nCapacity: {capacity}";
    }
}

// reception class (derived from event)
public class Reception : Event
{
    private string rsvpEmail;

    public Reception(string title, string description, string date, string time, Address address, string rsvpEmail)
        : base(title, description, date, time, address)
    {
        this.rsvpEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nRSVP Email: {rsvpEmail}";
    }
}

// outdoorgathering class (derived from event)
public class OutdoorGathering : Event
{
    private string weatherForecast;

    public OutdoorGathering(string title, string description, string date, string time, Address address, string weatherForecast)
        : base(title, description, date, time, address)
    {
        this.weatherForecast = weatherForecast;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nWeather Forecast: {weatherForecast}";
    }
}

// main program
class Program
{
    static void Main()
    {
        // creating address objects
        Address address1 = new Address("123 Main St", "Springfield", "IL", "USA");
        Address address2 = new Address("456 Maple Ave", "Toronto", "ON", "Canada");
        Address address3 = new Address("789 Oak St", "Los Angeles", "CA", "USA");

        // creating event objects
        Lecture lecture = new Lecture("Python Workshop", "A workshop for beginners.", "12/05/2024", "10:00 AM", address1, "Dr. John Doe", 50);
        Reception reception = new Reception("Holiday Party", "A festive holiday reception.", "12/20/2024", "6:00 PM", address2, "rsvp@company.com");
        OutdoorGathering outdoorGathering = new OutdoorGathering("Beach Picnic", "A relaxing day at the beach.", "12/15/2024", "12:00 PM", address3, "Sunny, 75°F");

        // creating a list of events
        List<Event> events = new List<Event> { lecture, reception, outdoorGathering };

        // displaying details for each event
        foreach (Event e in events)
        {
            Console.WriteLine(e.GetStandardDetails());
            Console.WriteLine("\nFull Details:\n" + e.GetFullDetails());
            Console.WriteLine("\nShort Description:\n" + e.GetShortDescription());
            Console.WriteLine("\n--------------------------------\n");
        }
    }
}