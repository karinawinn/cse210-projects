using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcome();
        string userName = PromptUserName();
        int userNumber = PromptUserNumber();
        int squareNumber = SquareNumber(userNumber);
        DisplayResult(userName,squareNumber);
    }
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the program!");
    }
    static string PromptUserName()
    {
        Console.Write("Enter your name:");
        string name = Console.ReadLine();
        return name;
    }
    static int PromptUserNumber()
    {
        Console.Write("Enter your favorite number:");
        int number = int.Parse(Console.ReadLine());
        return number;
    }
    static int SquareNumber(int number)
    {
        int squareNumber = number * number;
        return squareNumber;
    }
    static void DisplayResult(string userName, int squareNumber)
    {
        Console.WriteLine($"{userName}, the square of your number is {squareNumber}.");
    }
}