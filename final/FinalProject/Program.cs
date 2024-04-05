using System;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        List<string> options = ["Adiabatic Density", "Headwind/Tailwind", "Magnus Force", "Viscous/Collisional Drag"];
        Console.WriteLine("Projectile Motion Models");
        for (int i = 0; i < options.Count; i++) {
            Console.WriteLine($"{i+1}. {options[i]}");
        }
        Console.Write("What type of projectile motion would you like to model?");
        int response = int.Parse(Console.ReadLine());

        Console.Write("Starting Position (x,y):");
        string p = Console.ReadLine();
        string[] pos = p.Split(",");
        double[] position = [(double)float.Parse(pos[0]),(double)float.Parse(pos[1])];
        Console.Write("Starting Velocity:");
        double velocity = (double)float.Parse(Console.ReadLine());
        Console.Write("Launch Angle:");
        double angle = (double)float.Parse(Console.ReadLine());
        Console.Write("Mass:");
        double mass = (double)float.Parse(Console.ReadLine());
        Console.Write("Air Density:");
        double density = (double)float.Parse(Console.ReadLine());
        Console.Write("Cross Sectional Area:");
        double A = (double)float.Parse(Console.ReadLine());
        Console.Write("Drag Coefficient:");
        double C = (double)float.Parse(Console.ReadLine());

        (double[],double[]) results = ([(double)0],[(double)0]);
        if (response == 1) {
            Console.Write("Temperature (K):");
            double temperature = (double)float.Parse(Console.ReadLine());
            AdiabaticDensity model1 = new AdiabaticDensity(position,velocity,angle,mass,density,A,C,temperature);
            results = model1.CalcTrajectory();
            Console.WriteLine(results);
        }
        else if (response == 2) {
            Console.Write("Headwind (m/s):");
            double headwind = (double)float.Parse(Console.ReadLine());
            Console.Write("Tailwind (m/s):");
            double tailwind = (double)float.Parse(Console.ReadLine());
            HeadwindTailwind model2 = new HeadwindTailwind(position,velocity,angle,mass,density,A,C,headwind,tailwind);
            model2.CalcTrajectory();
        }
        else if (response == 3) {
            Console.Write("Angular Velocity (rad/s):");
            double angVelocity = (double)float.Parse(Console.ReadLine());
            MagnusForce model3 = new MagnusForce(position,velocity,angle,mass,density,A,C,angVelocity);
            model3.CalcTrajectory();
        }
        else if (response == 3) {
            Console.Write("Viscous Drag Coefficient:");
            double B1 = (double)float.Parse(Console.ReadLine());
            Console.Write("Collisional Drag Coefficient:");
            double B2 = (double)float.Parse(Console.ReadLine());
            ViscousCollision model4 = new ViscousCollision(position,velocity,angle,mass,density,A,C,B1,B2);
            model4.CalcTrajectory();
        }
        string printThis = "x position: [";
        foreach (double i in results.Item1) {
            if (i != results.Item1[^1]) {
                printThis = printThis + $"{i},";
            }
            else {
                printThis = printThis + $"{i}]";
            }
        }
        printThis = printThis + "]\n y position: [";
        foreach (double j in results.Item2) {
            printThis = "";
            if (j != results.Item2[^1]) {
                printThis = printThis + $"{j},";
            }
            else {
                printThis = printThis + $"{j}]";
            }
        }
        Console.WriteLine(printThis);
    }
}