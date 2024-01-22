using System;

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job("Del Taco", "Manager", 1706, 2020);

        Job job2 = new Job("Google", "CTO", 2020, 2024);

        Resume myResume = new Resume();
        myResume._name = "Karina Winn";
        myResume._jobs.Add(job1);
        myResume._jobs.Add(job2);

        myResume.Display();
    }
}