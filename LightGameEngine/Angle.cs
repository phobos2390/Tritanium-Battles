﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine
{
    public struct Angle
    {
        private double radians;
        private double degrees;

        private Angle(double degrees, double radians)
        {
            this.radians = radians;
            this.degrees = degrees;
        }

        public double Radians
        {
            get
            {
                return this.radians;
            }
            set
            {
                this.radians = value;
                this.degrees = this.radians * 180 / Math.PI;
            }
        }

        public double Degrees
        {
            get
            {
                return this.degrees;
            }
            set
            {
                this.degrees = value;
                this.radians = this.degrees * Math.PI / 180;
            }
        }

        public double Sine()
        {
            return Math.Sin(this.radians);
        }

        public double Cosine()
        {
            return Math.Cos(this.radians);
        }

        public static double Sine(Angle angle)
        {
            return angle.Sine();
        }

        public static double Cosine(Angle angle)
        {
            return angle.Cosine();
        }

        public static Angle CreateDegree(double degrees)
        {
            return new Angle(degrees, degrees * Math.PI / 180);
        }

        public static Angle CreateRadian(double radians)
        {
            return new Angle(radians * 180 / Math.PI, radians);
        }

        public static Angle ArcSine(double opposite, double hypotenuse)
        {
            if(hypotenuse != 0 && Math.Abs(opposite) > Math.Abs(hypotenuse))
            {
                return Angle.CreateRadian(Math.Asin(opposite / hypotenuse));
            }
            else
            {
                throw new DivideByZeroException();
            }
        }

        public static Angle ArcCosine(double opposite, double hypotenuse)
        {
            if (hypotenuse != 0 && Math.Abs(opposite) > Math.Abs(hypotenuse))
            {
                return Angle.CreateRadian(Math.Acos(opposite / hypotenuse));
            }
            else
            {
                throw new DivideByZeroException();
            }
        }
    }
}