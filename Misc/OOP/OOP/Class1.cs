using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Square
    {
        public string ShapeName { get; private set; }
        public double Area { get; private set; }
        public double Perimeter { get; private set; }
        public string CustomName { get; set; }

        private double _length;
        public double Length
        {
            get
            {
                return this._length;
            }
            set
            {
                this._length = value;
                this.Area = this.Length * this.Length;
                this.Perimeter = this.Length * 4;
            }
        }

        public double getArea()
        {
            Console.WriteLine($"{this.ShapeName} {this.CustomName} area is {this.Area}");
            return this.Area;
        }

        public double getPerimeter()
        {
            Console.WriteLine($"{this.ShapeName} {this.CustomName} perimeter is {this.Perimeter}");
            return this.Perimeter;
        }


        public Square(double length)
        {
            this.Length = length;
            this.ShapeName = "Square";
        }
    }

    
    public class Circle
    {
        public string ShapeName { get; private set; }
        public double Radius { get; private set; }
        public double Area { get; private set; }
        public double Circumference { get; private set; }

        public string CustomName { get; set; }
        private double _diameter;
        public double Diameter
        { 
            get
            {
                return this._diameter;
            }
            set
            {
                this._diameter = value;
                this.Radius = value / 2;
                this.Area = Math.PI * Math.Pow(this.Radius, 2);
                this.Circumference = Math.PI * 2 * this.Radius;
            }
        }

        public Circle(double diameter)
        {
            this.Diameter = diameter;
            this.ShapeName = "Circle";
        }

    }
    
}
