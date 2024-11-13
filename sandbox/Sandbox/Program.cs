using System;
using System.Collections.Generic;

public abstract class SmartDevice
{
    public string Name { get; set; }
    private bool isOn;
    private DateTime? timeTurnedOn;

    public SmartDevice(string name)
    {
        Name = name;
        isOn = false;
        timeTurnedOn = null;
    }

    public void TurnOn()
    {
        isOn = true;
        timeTurnedOn = DateTime.Now;
    }

    public void TurnOff()
    {
        isOn = false;
        timeTurnedOn = null;
    }

    public bool IsOn()
    {
        return isOn;
    }

    public TimeSpan? TimeOn()
    {
        if (isOn)
        {
            return DateTime.Now - timeTurnedOn;
        }
        return null;
    }

    public abstract void ReportStatus();
}

public class SmartLight : SmartDevice
{
    public SmartLight(string name) : base(name) { }

    public override void ReportStatus()
    {
        Console.WriteLine($"{Name} - Light Status: {(IsOn() ? "On" : "Off")} - Time on: {TimeOn()?.TotalMinutes ?? 0} minutes.");
    }
}

public class SmartHeater : SmartDevice
{
    public SmartHeater(string name) : base(name) { }

    public override void ReportStatus()
    {
        Console.WriteLine($"{Name} - Heater Status: {(IsOn() ? "On" : "Off")} - Time on: {TimeOn()?.TotalMinutes ?? 0} minutes.");
    }
}

public class SmartTV : SmartDevice
{
    public SmartTV(string name) : base(name) { }

    public override void ReportStatus()
    {
        Console.WriteLine($"{Name} - TV Status: {(IsOn() ? "On" : "Off")} - Time on: {TimeOn()?.TotalMinutes ?? 0} minutes.");
    }
}

public class Room
{
    public string Name { get; set; }
    public List<SmartDevice> Devices { get; set; }

    public Room(string name)
    {
        Name = name;
        Devices = new List<SmartDevice>();
    }

    public void AddDevice(SmartDevice device)
    {
        Devices.Add(device);
    }

    public void TurnOnAllDevices()
    {
        foreach (var device in Devices)
        {
            device.TurnOn();
        }
    }

    public void TurnOffAllDevices()
    {
        foreach (var device in Devices)
        {
            device.TurnOff();
        }
    }

    public void ReportAllDevices()
    {
        Console.WriteLine($"Room: {Name}");
        foreach (var device in Devices)
        {
            device.ReportStatus();
        }
    }

    public void ReportAllDevicesOn()
    {
        Console.WriteLine($"Devices On in {Name}:");
        foreach (var device in Devices)
        {
            if (device.IsOn())
            {
                device.ReportStatus();
            }
        }
    }

    public void ReportLongestRunningDevice()
    {
        SmartDevice longestRunning = null;
        TimeSpan? longestTime = null;

        foreach (var device in Devices)
        {
            var timeOn = device.TimeOn();
            if (timeOn.HasValue && (!longestTime.HasValue || timeOn.Value > longestTime.Value))
            {
                longestRunning = device;
                longestTime = timeOn;
            }
        }

        if (longestRunning != null)
        {
            Console.WriteLine($"Longest Running Device in {Name}: {longestRunning.Name} with {longestTime.Value.TotalMinutes} minutes.");
        }
        else
        {
            Console.WriteLine("No devices have been on long enough to report.");
        }
    }
}

public class House
{
    public List<Room> Rooms { get; set; }

    public House()
    {
        Rooms = new List<Room>();
    }

    public void AddRoom(Room room)
    {
        Rooms.Add(room);
    }

    public void TurnOnAllLights()
    {
        foreach (var room in Rooms)
        {
            foreach (var device in room.Devices)
            {
                if (device is SmartLight)
                {
                    device.TurnOn();
                }
            }
        }
    }

    public void TurnOffAllLights()
    {
        foreach (var room in Rooms)
        {
            foreach (var device in room.Devices)
            {
                if (device is SmartLight)
                {
                    device.TurnOff();
                }
            }
        }
    }

    public void ReportAllRooms()
    {
        foreach (var room in Rooms)
        {
            room.ReportAllDevices();
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        SmartLight light1 = new SmartLight("Living Room Light");
        SmartHeater heater1 = new SmartHeater("Living Room Heater");
        SmartTV tv1 = new SmartTV("Living Room TV");

        Room livingRoom = new Room("Living Room");
        livingRoom.AddDevice(light1);
        livingRoom.AddDevice(heater1);
        livingRoom.AddDevice(tv1);

        House house = new House();
        house.AddRoom(livingRoom);

        house.TurnOnAllLights();

        livingRoom.ReportAllDevices();

        livingRoom.TurnOffAllDevices();

        livingRoom.ReportAllDevicesOn();

        livingRoom.ReportLongestRunningDevice();
    }
}