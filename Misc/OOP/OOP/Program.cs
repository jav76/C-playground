using System;
using System.Collections.Generic;
using System.Linq;
using Shapes;

namespace MyApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Shapes.Square mySquare = new Shapes.Square(5);

            mySquare.CustomName = "Custom Square";
            mySquare.getArea();
            mySquare.getPerimeter();

            mySquare.Length = 12;
            mySquare.getArea();
            mySquare.getPerimeter();

            Shapes.Circle myCircle = new Shapes.Circle(5);
            Console.WriteLine(myCircle.Circumference);
            myCircle.Diameter = 6;
            Console.WriteLine(myCircle.Circumference);
        }
    }
}