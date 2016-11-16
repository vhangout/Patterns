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
            var input = Console.ReadLine();
            var stack = new Stack<string>();
            var result = CurrencyFactory.FirstHandler.validate(input, stack);
            if (result < 0)
                Console.WriteLine("Incorrect amount");
            else 
            {   
                Console.WriteLine("Result: ");
                while (stack.Count > 0)
                    Console.Write(stack.Pop() + " ");
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
