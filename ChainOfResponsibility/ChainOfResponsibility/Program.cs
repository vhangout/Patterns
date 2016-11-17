using System;
using System.Collections.Generic;
using System.Text;

namespace ChainOfResponsibility
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (CurrencyType type in Enum.GetValues(typeof(CurrencyType)))
            {
                if (type > CurrencyType.Invalid)
                {
                    Console.Write(type + ": ");
                    foreach(var amount in CurrencyFactory.config[type])
                    {
                        Console.Write(String.Format("{0,5}", amount) + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Usage: 1100rub, 1100RUB, 1100 rub, RUB 1100" + Environment.NewLine);
            var stack = new Stack<string>();
            ConsoleKeyInfo key;
            do
            {
                stack.Clear();
                Console.Write(">");
                var input = Console.ReadLine();
                var result = CurrencyFactory.FirstHandler.validate(input, stack);
                if (result < 0)
                    Console.WriteLine("Incorrect amount" + Environment.NewLine);
                else
                {
                    Console.WriteLine("Result: ");
                    while (stack.Count > 0)
                        Console.Write(stack.Pop() + " ");
                    Console.WriteLine(Environment.NewLine);
                }
                Console.WriteLine("For exit press Escape or other for continue." + Environment.NewLine);
                key = Console.ReadKey();
            } while (key.Key != ConsoleKey.Escape);
        }
    }
}
