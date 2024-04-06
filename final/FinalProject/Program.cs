using System;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        List<string> options = ["Adiabatic Density","Headwind/Tailwind","Magnus Force","Viscous/Collisional Drag","Combination","All"];
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
        }
        else if (response == 2) {
            Console.Write("Headwind (m/s):");
            double headwind = (double)float.Parse(Console.ReadLine());
            Console.Write("Tailwind (m/s):");
            double tailwind = (double)float.Parse(Console.ReadLine());
            HeadwindTailwind model2 = new HeadwindTailwind(position,velocity,angle,mass,density,A,C,headwind,tailwind);
            results = model2.CalcTrajectory();
        }
        else if (response == 3) {
            position = position.Append((double)float.Parse(pos[2])).ToArray();
            Console.Write("Angular Velocity (rad/s):");
            double angVelocity = (double)float.Parse(Console.ReadLine());
            MagnusForce model3 = new MagnusForce(position,velocity,angle,mass,density,A,C,angVelocity);
            results = model3.CalcTrajectory();
        }
        else if (response == 4) {
            Console.Write("Viscous Drag Coefficient:");
            double B1 = (double)float.Parse(Console.ReadLine());
            Console.Write("Collisional Drag Coefficient:");
            double B2 = (double)float.Parse(Console.ReadLine());
            ViscousCollision model4 = new ViscousCollision(position,velocity,angle,mass,density,A,C,B1,B2);
            results = model4.CalcTrajectory();
        }
        else if (response == 5) {
            double[] variables = [];
            Console.Write("Forces to Include:");
            string[] combo = Console.ReadLine().Split(",");
            foreach (string i in combo) {
                if (i == "Adiabatic Density") {
                    Console.Write("Temperature (K):");
                    double temperature = (double)float.Parse(Console.ReadLine());
                    variables = variables.Append(temperature).ToArray();
                }
                else if (i == "Headwind/Tailwind") {
                    Console.Write("Headwind (m/s):");
                    double headwind = (double)float.Parse(Console.ReadLine());
                    Console.Write("Tailwind (m/s):");
                    double tailwind = (double)float.Parse(Console.ReadLine());
                    variables = variables.Append(headwind).ToArray();
                    variables = variables.Append(tailwind).ToArray();
                }
                else if (i == "Magnus Force") {
                    position = position.Append((double)float.Parse(pos[2])).ToArray();
                    Console.Write("Angular Velocity (rad/s):");
                    double angVelocity = (double)float.Parse(Console.ReadLine());
                    variables = variables.Append(angVelocity).ToArray();
                }
                else if (i == "Viscous/Collisional Drag") {
                    Console.Write("Viscous Drag Coefficient:");
                    double B1 = (double)float.Parse(Console.ReadLine());
                    Console.Write("Collisional Drag Coefficient:");
                    double B2 = (double)float.Parse(Console.ReadLine());
                    variables = variables.Append(B1).ToArray();
                    variables = variables.Append(B2).ToArray();
                }    
            }
            Combo model5 = new Combo(position,velocity,angle,mass,density,A,C,combo,variables);
            results = model5.CalcTrajectory();
        }
        else if (response == 6) {
            position = position.Append((double)float.Parse(pos[2])).ToArray();
            Console.Write("Temperature (K):");
            double temperature = (double)float.Parse(Console.ReadLine());
            Console.Write("Headwind (m/s):");
            double headwind = (double)float.Parse(Console.ReadLine());
            Console.Write("Tailwind (m/s):");
            double tailwind = (double)float.Parse(Console.ReadLine());
            Console.Write("Angular Velocity (rad/s):");
            double angVelocity = (double)float.Parse(Console.ReadLine());
            Console.Write("Viscous Drag Coefficient:");
            double B1 = (double)float.Parse(Console.ReadLine());
            Console.Write("Collisional Drag Coefficient:");
            double B2 = (double)float.Parse(Console.ReadLine());
            All model6 = new All(position,velocity,angle,mass,density,A,C,temperature,headwind,tailwind,angVelocity,B1,B2);
            results = model6.CalcTrajectory();
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
        Console.WriteLine("");
        Console.WriteLine(printThis);
        Console.WriteLine("");
        printThis = "\n y position: [";
        foreach (double j in results.Item2) {
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