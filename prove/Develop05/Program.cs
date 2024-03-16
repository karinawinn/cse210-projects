using System;
using System.Runtime.InteropServices;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        List<Goal> goals = new List<Goal>();
        List<string> options = new List<string>(["1. Create New Goal","2. List Goals","3. Save Goals","4. Load Goals","5. Record Event","6. Quit"]);
        string again = "yes";
        int total = 0;
        int level = 0;
        int remainder = 100;
        while (again == "yes") {
            Console.WriteLine($"\nYou have {total} points.");
            Console.WriteLine($"\nYou are level {level}. {remainder} points to the next level.");
            Console.WriteLine($"\nMenu Options:");
            foreach (string option in options) {
                Console.WriteLine(option);
            }
            Console.Write("Select a choice from the menu:");
            int selection = int.Parse(Console.ReadLine());
            if (selection == 1) {
                Console.WriteLine("The types of Goals are:");
                Console.WriteLine("1. Simple Goal");
                Console.WriteLine("2. Eternal Goal");
                Console.WriteLine("3. Checklist Goal");
                Console.Write("What type of goal would you like to create?");
                int choice = int.Parse(Console.ReadLine());
                Console.Write("What is the name of your goal?");
                string name = Console.ReadLine();
                Console.Write("What is a short description of it?");
                string description = Console.ReadLine();
                Console.Write("What is the amount of points associated with this goal?");
                int points = int.Parse(Console.ReadLine());
                if (choice == 1) {
                    goals.Add(new Simple(name,description,points));
                }
                if (choice == 2) {
                    goals.Add(new Eternal(name,description,points));
                }
                if (choice == 3) {
                    Console.Write("How many times does this goal need to be accomplished for a bonus?");
                    int number = int.Parse(Console.ReadLine());
                    Console.Write("What is the bonus for accomplishing it that many times?");
                    int bonus = int.Parse(Console.ReadLine());
                    goals.Add(new Checklist(name,description,points,number,bonus));
                }
            }
            else if (selection == 2) {
                Console.WriteLine("The goals are:");
                int count = 0;
                foreach (Goal goal in goals) {
                    count += 1;
                    Console.WriteLine($"{count}. {goal}");
                }
            }
            else if (selection == 3) {
                Console.Write("What is the filename for the goal file?");
                string fileName = Console.ReadLine();
                using (StreamWriter outputFile = new StreamWriter(fileName)) {
                    outputFile.WriteLine($"{total},{level},{remainder}");
                    foreach (Goal goal in goals) {
                        string output = goal.GetStringRepresentation();
                        outputFile.WriteLine(output);
                    }
                }
            }
            else if (selection == 4) {
                Console.Write("What is the filename for the goal file?");
                string fileName = Console.ReadLine();
                string[] lines = System.IO.File.ReadAllLines(fileName);
                int count = 0;
                foreach (string line in lines) {
                    count += 1;
                    if (count == 1) {
                        string[] stats = line.Split(",");
                        total = int.Parse(stats[0]);
                        level = int.Parse(stats[1]);
                        remainder = int.Parse(stats[2]);
                    }
                    else {
                        string[] split1 = line.Split(":");
                        string classType = split1[0];
                        string parameters = split1[1];
                        string[] split2 = parameters.Split(",");
                        string name = split2[0];
                        string description = split2[1];
                        int points = int.Parse(split2[2]);

                        if (classType == "SimpleGoal") {
                            string status = split2[3];
                            goals.Add(new Simple(name,description,points,status));
                        }
                        else if (classType == "ChecklistGoal") {
                            int bonus = int.Parse(split2[3]);
                            int toComplete = int.Parse(split2[4]);
                            int eventCount = int.Parse(split2[5]);
                            goals.Add(new Checklist(name,description,points,bonus,toComplete,eventCount));
                        }
                        else if (classType == "EternalGoal") {
                            goals.Add(new Eternal(name,description,points));
                        }
                    }
                }
                level = total / 100;
                remainder = 100 - (total - (level*100));
            }
            else if (selection == 5) {
                Console.WriteLine("The goals are:");
                int count = 0;
                foreach (Goal goal in goals) {
                    count += 1;
                    string name = goal.GetName();
                    Console.WriteLine($"{count}. {name}");
                }
                Console.Write("Which goal did you accomplish?");
                int response = int.Parse(Console.ReadLine());
                goals[response - 1].RecordEvent();
                int points = goals[response - 1].GetPoints();
                Console.WriteLine($"Congratulations! You have earned {points} points!");
                total += points;
                Console.WriteLine($"You now have {total} points");
                int newLevel = total / 100;
                if (newLevel > level) {
                    Console.WriteLine("Congratulations! You have leveled up!");
                }
                level = newLevel;
                remainder = 100 - (total - (level*100));
            }
            else {
                again = "no";
            }
        }
    }
}