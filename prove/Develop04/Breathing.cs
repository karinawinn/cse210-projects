using System.Security.Principal;

class Breathing : Activity {
    public Breathing() {
        description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
        duration = 30;
        name = "Breathing";
        stats = 0;
        total = 0;
    }
    public override void Play() {
        PrintBegMessage(name, description);
        Console.Clear();
        Loading(5,"yes", "Get ready...");
        Console.WriteLine(" ");
        int elapsed = 0;
        while (elapsed < duration) {
            if ((duration - elapsed) % 10 != 0) {
                int remainder = duration % 10;
                Countdown(remainder/2,"yes", "Breathe in...");
                Countdown(remainder/2 + 1,"yes", "Breathe out...");
                elapsed += remainder;
            }
            else {
                Countdown(4,"yes", "Breathe in...");
                Countdown(6,"yes", "Breathe out...");
                elapsed += 10;
            }
        }
        PrintEndMessage(name, duration);
        stats += 1;
        total += duration;
    }
    public override void DisplayStats() {
        Console.WriteLine($"{name}:");
        Console.WriteLine($"Times Played: {stats}");
        Console.WriteLine($"Total Time Played: {total} seconds");
    }
}