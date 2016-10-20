using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactory.CarsAndParts
{
    public class Car
    {
        public Wheel wheel { get; set; }

        public CarBody carBody { get; set; }

        public string Name { get; private set; }
        
        public string Image{
            get {
                var result = new StringBuilder();
                carBody.Image.ForEach(line => result.AppendLine(line));
                wheel.Image.ForEach(line =>
                    result.Append("".PadRight(carBody.LeftWheelOffset))
                        .Append(line.PadRight(carBody.Wheelbase, ' '))
                        .AppendLine(line)
                );
                return result.ToString();
            }
        }

        public Car(string name)
        {
            Name = name;
        }
    }
}
