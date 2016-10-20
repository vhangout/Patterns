using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactory.CarsAndParts
{
    public abstract class CarPart
    {
        public int Height { get; protected set; }
        public int Width { get; protected set; }
        protected string _image;
        public List<string> Image { 
            get {
                return Enumerable.Range(0, Height)
                    .Select(i => _image.Substring(i * Width, Width))
                    .ToList<string>();
            }
        }
    }

    public abstract class Wheel : CarPart {}

    public abstract class CarBody : CarPart 
    {
        public int Wheelbase { get; protected set; }
        public int LeftWheelOffset { get; protected set; }
    }

    public class BigWheel : Wheel 
    {
        public BigWheel()
        {
            Width = 7;
            Height = 6;
            _image = "  | |   ##### ##   #### @ ####   ## ##### ";
        }
        
    }

    public class SmallWheel : Wheel
    {
        public SmallWheel()
        {
            Width = 3;
            Height = 3;
            _image = " # # # # ";
        }

    }

    public class BigBody : CarBody
    {
        public BigBody()
        {
            Width = 44;
            Height = 6;
            LeftWheelOffset = 6;
            Wheelbase = 24;
            _image = "                         ############       " +
                "                         ##############     " +
                "                         #################  " +
                "############################################" +
                "############################################" +
                "############################################";
            
        }
    }

    public class SmallBody : CarBody
    {
        public SmallBody()
        {
            Width = 26;
            Height = 3;
            LeftWheelOffset = 1;
            Wheelbase = 18;
            _image = "     #############        " +
                "######################### " +
                "##########################";

        }
    }
}
