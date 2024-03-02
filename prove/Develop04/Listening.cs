using System.Reflection.Metadata.Ecma335;

class Listening : Activity {
    List<string> prompts;
    public Listening() {
        name = "Listening";
        duration = 30;
        description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
        prompts = ["Who are people that you appreciate?","What are personal strengths of yours?","Who are people that you have helped this week?","When have you felt the Holy Ghost this month?","Who are some of your personal heroes?"];
        stats = 0;
        total = 0;
    }
    public override void Play()
    {
        PrintBegMessage(name, description);
        Console.WriteLine(" ");
        Loading(5,"yes","Get ready...");
        Random randomGenerator = new Random();
        int number = randomGenerator.Next(0, prompts.Count);
        string prompt = prompts[number];

        Console.WriteLine(" ");
        Console.WriteLine("List as many responses you can to the following prompt:");
        Console.WriteLine(" ");
        Console.WriteLine($" --- {prompt} ---");
        Console.WriteLine(" ");
        Countdown(5,"yes","You may begin in: ");

        int count = 0;
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(duration);
        while (DateTime.Now < endTime) {
            Console.Write("> ");
            string response = Console.ReadLine();
            count += 1;
        }
        Console.WriteLine(" ");
        Console.WriteLine($"You listed {count} items!");
        Console.WriteLine(" ");
        PrintEndMessage(name, duration);
        stats += 1;
        total += duration;
    }
    public override void DisplayStats()
    {
        Console.WriteLine($"{name}:");
        Console.WriteLine($"Times Played: {stats}");
        Console.WriteLine($"Total Time Played: {total} seconds");
    }
}