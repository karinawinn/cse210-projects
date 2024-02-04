using System;
using System.ComponentModel.Design;

class Program
{
    static void Main(string[] args)
    {
        int selection = 0;
        int prevSelection = 0;
        Journal newJournal = new Journal();
        newJournal.entries = new List<Entry>();
        Journal loadJournal = new Journal();
        while (selection != 5) {
            List<string> options = ["1. Write", "2. Display", "3. Load", "4. Save", "5. Quit"];
            foreach (string option in options) {
                Console.WriteLine(option);
            }
            Console.Write("What would you like to do?");
            selection = int.Parse(Console.ReadLine());

            if (selection == 1) {
                Prompt newPrompt = new Prompt();
                Console.Write($"Today's Event:");
                string specialDate = Console.ReadLine();
                string prompt = newPrompt.PromptGenerator(newPrompt.promptList);
                Console.Write($"{prompt}");
                string response = Console.ReadLine();
                string date = newPrompt.GetDate();
                Entry newEntry = new Entry(date, specialDate, prompt, response);
                newJournal.entries.Add(newEntry);
                prevSelection = 1;
            }
            else if (selection == 2) {
                if (prevSelection == 1) {
                    newJournal.Display(newJournal.entries);
                }
                else if (prevSelection == 3) {
                    loadJournal.Display(loadJournal.entries);
                }
            }
            else if (selection == 3) {
                File file = new File();
                Console.Write("What is the filename?");
                file.fileName = Console.ReadLine();
                loadJournal = file.Load(file.fileName);
                prevSelection = 3;
            }
            else if (selection == 4) {
                File file = new File();
                Console.Write("What is the filename?");
                file.fileName = Console.ReadLine();
                file.Save(file.fileName, newJournal.entries);
            }
            else if (selection == 5) {
                break;
            }
        }
    }
}