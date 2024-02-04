public class Prompt {
    public List<string> promptList = ["Who was the most interesting person I interacted with today?",
     "What was the best part of my day?", "How did I see the hand of the Lord in my life today?",
      "What was the strongest emotion I felt today?", "If I had one thing I could do over today, what would it be?",
      "What was one thing I learned today?", "What is one thing that I can do better tomorrow?", "Who did I help today?"];

    public Prompt() {
        List<string> prompts = promptList;
    }
    public string PromptGenerator(List<string> prompts) {
        Random randomGenerator = new Random();
        int number = randomGenerator.Next(0, 6);
        string entryPrompt = prompts[number];
        return entryPrompt;
    }
    public string GetDate() {
        DateTime theCurrentTime = DateTime.Now;
        string date = theCurrentTime.ToShortDateString();
        return date;
    }
    public void Display(string entryPrompt) {
        Console.WriteLine($"{entryPrompt}");
    }
}