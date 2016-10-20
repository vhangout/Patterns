using System;
using System.Collections.Generic;
using System.Text;
using AbstractFactory.CarsAndParts;
using AbstractFactory.Factory;
using System.Linq;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            var factories = new List<ICarFactory>() { 
                new BigCarFactory(),
                new SmallCarFactory(),
                new MonsterCarFactory(),
                new WTFCarFactory()
            };

            Car car;

            factories.ForEach(delegate(ICarFactory factory)
            {
                car = factory.CreateCar();
                Console.WriteLine();
                Console.WriteLine(car.Name);
                Console.WriteLine();
                Console.WriteLine(car.Image);
                Console.WriteLine();
             });

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
