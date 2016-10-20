using AbstractFactory.CarsAndParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Factory
{
    interface ICarFactory
    {
        Car CreateCar();
    }

    public class BigCarFactory : ICarFactory
    {
        public Car CreateCar()
        {
            var car = new Car("Ford F-150");
            car.carBody = new BigBody();
            car.wheel = new BigWheel();
            return car;
        }
    }

    public class SmallCarFactory : ICarFactory
    {
        public Car CreateCar()
        {
            var car = new Car("Nissan Sentra SV 4-cyl CVT");
            car.carBody = new SmallBody();
            car.wheel = new SmallWheel();
            return car;
        }
    }

    public class MonsterCarFactory : ICarFactory
    {
        public Car CreateCar()
        {
            var car = new Car("Monster Car");
            car.carBody = new SmallBody();
            car.wheel = new BigWheel();
            return car;
        }
    }

    public class WTFCarFactory : ICarFactory
    {
        public Car CreateCar()
        {
            var car = new Car("WTF");
            car.carBody = new BigBody();
            car.wheel = new SmallWheel();
            return car;
        }
    }
}
