using System.Security.Cryptography.X509Certificates;

public class Entry {

    public string promptResponse;

    public string date;

    public string prompt;

    public string specialDate;

    public Entry(string date, string specialDate, string prompt, string promptResponse) {
        this.date = date;
        this.prompt = prompt;
        this.promptResponse = promptResponse;
        this.specialDate = specialDate;
    }
    public void Display(string date, string specialDate, string prompt, string promptResponse) {
        Console.WriteLine($"Date: {date} - Event: {specialDate}");
        Console.WriteLine($"Prompt: {prompt}");
        Console.WriteLine($"{promptResponse}");
    }
}