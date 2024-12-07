using System;
using System.Collections.Generic;

class Comment
{
    // attributes for comment
    public string CommenterName { get; set; }
    public string Text { get; set; }

    // constructor to initialize comment
    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }
}

class Video
{
    // attributes for video
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; } // length in seconds
    private List<Comment> Comments { get; set; }

    // constructor to initialize video
    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    // method to add a comment to the video
    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    // method to get the number of comments
    public int GetCommentCount()
    {
        return Comments.Count;
    }

    // method to display video details and its comments
    public void DisplayInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Number of Comments: {GetCommentCount()}");
        Console.WriteLine("Comments:");
        foreach (var comment in Comments)
        {
            Console.WriteLine($"  {comment.CommenterName}: {comment.Text}");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // create instances of video
        var video1 = new Video("Exploring C# Classes", "Code Academy", 600);
        var video2 = new Video("Understanding Abstraction", "Tech Tutorials", 450);
        var video3 = new Video("C# for Beginners", "LearnCoding", 300);

        // add comments to video1
        video1.AddComment(new Comment("Alice", "Great explanation!"));
        video1.AddComment(new Comment("Bob", "Very helpful, thanks."));
        video1.AddComment(new Comment("Charlie", "Looking forward to more videos."));

        // add comments to video2
        video2.AddComment(new Comment("Dave", "Abstraction made simple."));
        video2.AddComment(new Comment("Eve", "Loved the examples!"));

        // add comments to video3
        video3.AddComment(new Comment("Frank", "Perfect for beginners."));
        video3.AddComment(new Comment("Grace", "Clear and concise."));
        video3.AddComment(new Comment("Hank", "Awesome tutorial!"));

        // add all videos to a list
        var videos = new List<Video> { video1, video2, video3 };

        // iterate through the list and display each video's details and comments
        foreach (var video in videos)
        {
            video.DisplayInfo();
        }
    }
}