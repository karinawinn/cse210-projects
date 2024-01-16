using System;
using System.Net;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        int number = randomGenerator.Next(1, 101);
        string again = "yes";
        while (again == "yes")
        {
            Console.Write("What is your guess?");
            int guess = int.Parse(Console.ReadLine());
            if (guess > number) 
            {
                Console.WriteLine("Lower");
                again = "yes";
            }
            else if (guess < number)
            {
                Console.WriteLine("Higher");
                again = "yes";
            }
            else if (guess == number)
            {
                Console.WriteLine("You guessed the number!");
                again = "no";
            }
        }

    }
}