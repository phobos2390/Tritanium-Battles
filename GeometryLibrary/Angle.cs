using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryLibrary
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

        public void AddAngle(Angle angle)
        {
            this.Degrees += angle.degrees;
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
            if(hypotenuse != 0 && Math.Abs(opposite) <= Math.Abs(hypotenuse))
            {
                return Angle.CreateRadian(Math.Asin(opposite / hypotenuse));
            }
            else
            {
                throw new DivideByZeroException();
            }
        }

        public static Angle ArcCosine(double adjacent, double hypotenuse)
        {
            if (hypotenuse != 0 && Math.Abs(adjacent) <= Math.Abs(hypotenuse))
            {
                return Angle.CreateRadian(Math.Acos(adjacent / hypotenuse));
            }
            else
            {
                throw new DivideByZeroException();
            }
        }

        public static Vector3d ZVector(Angle pitch, Angle yaw)
        {
            return ZVector(pitch, yaw, 1);
        }

        public static Vector3d ZVector(Angle pitch, Angle yaw, double scalar)
        {
            return new Vector3d(
                -pitch.Sine() * scalar,
                yaw.Sine() * pitch.Cosine() * scalar,
                -yaw.Cosine() * pitch.Cosine() * scalar);
        }

        public static Quaterniond ZOrientation(Angle pitch, Angle yaw, Angle roll)
        {
            return Quaterniond.FromAxisAngle(ZVector(pitch, yaw), roll.Radians);
        }

        public static Quaterniond XOrientation(Angle pitch, Angle yaw, Angle deltaYaw)
        {
            return Quaterniond.FromAxisAngle(XVector(pitch, yaw), deltaYaw.Radians);
        }

        public static Vector3d XVector(Angle pitch, Angle yaw)
        {
            return new Vector3d(
                pitch.Cosine(),
                yaw.Sine() * pitch.Sine(),
                -yaw.Cosine() * pitch.Sine());
        }

        public static Tuple<Angle,Angle> AngleOfVector(Vector3d vector)
        {
            Vector3d vec = vector.Normalized();
            return Tuple.Create<Angle,Angle>(Angle.ArcSine(vec.X,1), Angle.ArcCosine(-vec.Z, Math.Sqrt(1 - vec.X * vec.X)));
        }

        public void Divide(double scalar)
        {
            Degrees = degrees / scalar;
        }

        public void Multiply(double scalar)
        {
            Degrees = degrees * scalar;
        }
    }
}
