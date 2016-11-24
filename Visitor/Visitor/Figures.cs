using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor
{
    public abstract class Figure
    {
        public abstract void Accept(IVisitor visitor);
    }

    public class Rectangle : Figure
    {
        private int _width;
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = Math.Abs(value);
            }
        }

        private int _height;
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = Math.Abs(value);
            }
        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
            Console.WriteLine("Create rectangle by size: {0}, {1}", Width, Height);
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

    }

    public class Triangle : Figure
    {
        private int _side1;
        public int Side1
        { 
            get
            {
                return _side1;
            }
            set
            {
                _side1 = Math.Abs(value);
            }
        }

        private int _side2;
        public int Side2
        {
            get
            {
                return _side2;
            }
            set
            {
                _side2 = Math.Abs(value);
            }
        }
        
        private double _angle;
        public double Angle 
        {
            get
            {
                return _angle;
            }

            set
            {
                _angle = Math.Abs(value % 180);
            }
        }

        public Triangle(int side1, int side2, float angle)
        {
            Side1 = side1;
            Side2 = side2;
            Angle = angle;
            Console.WriteLine("Create triangle (side1, side2, angle): {0}, {1}, {2}", Side1, Side2, Angle);
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Dot : Figure
    {
        public Dot()
        {
            Console.WriteLine("Create dot");
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
