using System;

class Program
{
    static void Main(string[] args)
    {
        List<Shape> shapes = new List<Shape>();

        shapes.Add(new Square("Red",4));
        shapes.Add(new Rectangle("Blue",4,5));
        shapes.Add(new Circle("Green",4));

        foreach (Shape shape in shapes) {
            double area = shape.GetArea();
            string color = shape.GetColor();
            Console.WriteLine($"Shape: {shape}, Color: {color}, Area: {area}");
        }
    }
}