using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage?");
        string answer = Console.ReadLine();
        int gradePercentage = int.Parse(answer);
        string letter = " ";
        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80 && gradePercentage < 90)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70 && gradePercentage < 80)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60 && gradePercentage < 70)
        {
            letter = "D";
        }
        else if (gradePercentage < 60)
        {
            letter = "F";
        }
        Console.WriteLine($"Your grade is a {letter}.");
        if (gradePercentage >= 70)
        {
            Console.WriteLine("Congrats! You passed the class!");
        }
        else if (gradePercentage < 70)
        {
            Console.WriteLine("You didn't pass the class. Better luck next time!");
        }
    }
}