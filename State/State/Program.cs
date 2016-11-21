using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace State
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Для выхода нажмите Ctrl+C" + Environment.NewLine);
            new Copier().RunMachine();
        }
    }
}
