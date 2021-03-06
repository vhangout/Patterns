﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor
{
    struct Point {
        public int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public interface IVisitor
    {
        string getTitle();
        void Visit(Rectangle figure);
        void Visit(Triangle figure);
        void Visit(Dot figure);
    }

    public class AreaVisitor : IVisitor
    {
        public string getTitle()
        {
            return "Calculate Area";
        }

        public void Visit(Rectangle figure)
        {
            Console.WriteLine("Rectangle area: " + (figure.Width * figure.Height));
        }

        public void Visit(Triangle figure)
        {
            Console.WriteLine("Triangle area: {0:0.000}",
                (0.5 * figure.Side1 * figure.Side2 * Math.Sin((Math.PI / 180) * figure.Angle)));
        }

        public void Visit(Dot figure)
        {
            Console.WriteLine("Dot area is 0");
        }
    }

    public class DrawVisitor : IVisitor
    {
        public int X { get; set; }
        public int Y { get; set; }

        public DrawVisitor(int x, int y)
        {
            X = x;
            Y = y;
        }

        public string getTitle()
        {
            return string.Format("Draw figure at {0}, {1}", X, Y);
        }

        private void Draw(List<Point> points)
        {
            if (points.Count == 1)
            {
                Console.WriteLine("\tDot at {0}, {1}", points[0].x, points[0].y);
            }
            else
            {
                for (int i = 0; i < points.Count - 1; i++)
                {
                    Console.WriteLine("\tLine from {0}, {1} to {2}, {3}",
                        points[i].x, points[i].y, points[i + 1].x, points[i + 1].y);
                }
                Console.WriteLine("\tLine from {0}, {1} to {2}, {3}",
                        points[points.Count - 1].x, points[points.Count - 1].y, points[0].x, points[0].y);
            }
        }

        public void Visit(Rectangle figure)
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(X, Y));
            points.Add(new Point(X + figure.Width, Y));
            points.Add(new Point(X + figure.Width, Y + figure.Height));
            points.Add(new Point(X, Y + figure.Height));
            Console.WriteLine("Draw rectangle:");
            Draw(points);
        }

        public void Visit(Triangle figure)
        {
            List<Point> points = new List<Point>();
            double angle = Math.Sin((Math.PI / 180) * figure.Angle);
            points.Add(new Point(X, Y));
            points.Add(new Point((int)(X + figure.Side1 * Math.Cos(0.5 * angle)), 
                (int)(Y - figure.Side1 * Math.Cos(0.5 * angle))));
            points.Add(new Point((int)(X + figure.Side2 * Math.Cos(0.5 * angle)),
                (int)(Y + figure.Side2 * Math.Cos(0.5 * angle))));
            Console.WriteLine("Draw triangle:");
            Draw(points);
        }

        public void Visit(Dot figure)
        {
            Console.WriteLine("Draw Dot:");
            Draw(new List<Point>() { new Point(X, Y) });
        }
    }

    public class ScaleVisitor : IVisitor
    {
        private int _scale;
        public int Scale {
            get 
            {
                return _scale;
            }
            set
            {
                _scale = Math.Abs(value);
            }
        }

        public ScaleVisitor(int scale) {
            Scale = scale;
        }

        public string getTitle()
        {
            return string.Format("Scale figure by {0}", Scale);
        }

        public void Visit(Rectangle figure)
        {
            Console.Write("Scale rectangle from {0}, {1} ", figure.Width, figure.Height);
            figure.Width *= Scale;
            figure.Height *= Scale;
            Console.WriteLine("to {0}, {1} ", figure.Width, figure.Height);
        }

        public void Visit(Triangle figure)
        {
            Console.Write("Scale triangle sides from {0}, {1} ", figure.Side1, figure.Side2);
            figure.Side1 *= Scale;
            figure.Side2 *= Scale;
            Console.WriteLine("to {0}, {1} ", figure.Side1, figure.Side2);
        }

        public void Visit(Dot figure)
        {
            Console.WriteLine("Dot isn't scaled");
        }
    }
}
