using System.Timers;

abstract class Activity {
    protected int duration;
    protected string name;
    protected string description;
    protected int stats;
    protected int total;
    public abstract void Play();
    public void PrintBegMessage(string name, string description) {
        Console.Clear();
        Console.WriteLine($"Welcome to the {name} Activity.");
        Console.WriteLine(" ");
        Console.WriteLine($"{description}");
        Console.WriteLine(" ");
        Console.Write("How long, in seconds, would you like for your session?");
        duration = int.Parse(Console.ReadLine());
    }
    public void PrintEndMessage(string name, int duration) {
        Loading(10,"yes","Well done!!!");
        Console.WriteLine(" ");
        string message = "You have completed another " + duration + " seconds of the " + name + " Activity";
        Loading(10,"yes",message);
    }
    protected virtual void Loading(int time, string option, string message) {
        if (option == "yes") {
            Console.WriteLine(message);
        }
        int runTime = 0;
        while (runTime < time) {
            Console.Write("-");
            Thread.Sleep(500);
            Console.Write("\b \b");
            Console.Write("\\"); 
            Thread.Sleep(500);
            Console.Write("\b \b");
            Console.Write("|"); 
            Thread.Sleep(500);
            Console.Write("\b \b");
            Console.Write("/");
            Thread.Sleep(500);
            Console.Write("\b \b");
            runTime += 2;
        }
    }
    protected virtual void Countdown(int time, string option, string message) {
        int elapsed = 0;
        if (option == "yes") {
            Console.WriteLine($"{message}");
        }
        while (elapsed < time) {
            Console.Write("\b \b");
            Console.Write($"{time - elapsed}");
            Thread.Sleep(1000);
            elapsed += 1;
        }
        Console.WriteLine(" ");
        Console.WriteLine(" ");
    }
    public virtual void DisplayStats() {
        Console.WriteLine($"{name}:");
        Console.WriteLine($"Times Played: {stats}");
        Console.WriteLine($"Total Time Played: {total} seconds");
    }
}