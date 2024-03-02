using System.Diagnostics.CodeAnalysis;

class Reflecting : Activity {
    List<string> prompts;
    List<string> questions;
    public Reflecting() {
        name = "Reflecting";
        description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
        duration = 30;
        prompts = ["Think of a time when you stood up for someone else.","Think of a time when you did something really difficult","Think of a time when you helped someone in need.","Think of a time when you did something truly selfless."];
        questions = ["Why was this experience meaningful to you?","Have you ever done anything like this before?","How did you get started?","How did you feel when it was complete?","What made this time different than other times when you were not as successful?","What is your favorite thing about this experience?","What could you learn from this experience that applies to other situations?","What did you learn about yourself through this experience?","How can you keep this experience in mind in the future?"];
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

        Console.Clear();
        Console.WriteLine("Consider the following prompt:");
        Console.WriteLine(" ");
        Console.WriteLine($" --- {prompt} ---");
        Console.WriteLine(" ");
        Console.Write("When you have something in mind, press enter to continue.");
        string cont = Console.ReadLine();
        Console.WriteLine(" ");
        Console.WriteLine("Now ponder on each of the following questions as they related to this experience.");
        Console.WriteLine(" ");
        Countdown(5,"yes","You may begin in: ");

        Console.Clear();
        int numberQuestions = 0;
        while (numberQuestions < 2) {
            number = randomGenerator.Next(0, questions.Count);
            string question = questions[number];
            Console.WriteLine($"> {question}");
            Loading(duration/2,"no","");
            Console.WriteLine(" ");
            numberQuestions += 1;
        }
        PrintEndMessage(name, duration);
        stats += 1;
        total += duration;

    }
    public override void DisplayStats()
    {
        Console.WriteLine($"{name} Activity:");
        Console.WriteLine($"Times Played: {stats}");
        Console.WriteLine($"Total Time Played: {total} seconds");
    }
}