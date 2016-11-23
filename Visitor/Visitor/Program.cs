using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Figure[] figures = new Figure[] {
                new Rectangle(10, 15), 
                new Triangle(10, 10, 60),
                new Dot(),
                new Triangle(-10, 15, 270)
            };

            IVisitor[] visitors = new IVisitor[] {
                new AreaVisitor(),
                new DrawVisitor(100, 100),
                new ScaleVisitor(2)
            };

            
            foreach (var visitor in visitors)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n**********************");
                var color = 5;
                foreach (var figure in figures)
                {
                    Console.ForegroundColor = (ConsoleColor)Enum.ToObject(typeof(ConsoleColor), color);
                    figure.Accept(visitor);
                    Console.WriteLine();
                    color++;
                }                
            }

            Console.ReadKey();
        }
    }
}
