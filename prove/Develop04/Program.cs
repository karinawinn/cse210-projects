using System;
using System.ComponentModel.Design;

class Program
{
    static void Main(string[] args)
    {
        int selection = 0;
        Activity[] activities = { new Breathing(), new Reflecting(), new Listening() };

        while (selection != 5) {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Start Breathing Activity");
            Console.WriteLine("2. Start Reflecting Activity");
            Console.WriteLine("3. Start Listening Activity");
            Console.WriteLine("4. Display Game Statistics");
            Console.WriteLine("5. Quit");
            Console.Write("Select a choice from the menu:");
            selection = int.Parse(Console.ReadLine());

            if (selection >= 1 && selection <= 3) {
                activities[selection - 1].Play();
            }
            else if (selection == 4) {
                foreach (Activity activity in activities) {
                    activity.DisplayStats();
                }
            }
            else {
                break;
            }
            // To exceed requirements, I added code that would track and display 
            // summary statistics for each game. 
        }  
    }
}