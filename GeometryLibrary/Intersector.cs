using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryLibrary
{
    public class Intersector
    {
        public static List<double> SolveCubicEquation(double a, double b, double c, double d)
        {
            List<double> zeroes = new List<double>();
            double discriminant = 18 * a * b * c * d 
                                -  4 * b * b * b * d 
                                +      b * b * c * c 
                                -  4 * a * c * c * c 
                                - 27 * a * a * d * d;
            if(discriminant > 0)
            {
                
            }
            else if(discriminant == 0)
            {

            }
            else
            {

            }
            return zeroes;
        }

        public static double CalculateCPASqr(Vector3d firstPoint, Vector3d firstVelocity, Vector3d secondPoint, Vector3d secondVelocity)
        {
            double tCPA = calculateCPATime(firstPoint, firstVelocity, secondPoint, secondVelocity);
            Vector3d displacementVector = (firstPoint + Vector3d.Multiply(firstVelocity, tCPA)) - (secondPoint + Vector3d.Multiply(secondVelocity, tCPA));
            return displacementVector.LengthSquared;
        }

        public static double CalculateCPA(Vector3d firstPoint, Vector3d firstVelocity, Vector3d secondPoint, Vector3d secondVelocity)
        {
            double tCPA = calculateCPATime(firstPoint, firstVelocity, secondPoint, secondVelocity);
            Vector3d displacementVector = (firstPoint + Vector3d.Multiply(firstVelocity, tCPA)) - (secondPoint + Vector3d.Multiply(secondVelocity, tCPA));
            return displacementVector.Length;
        }

        private static double calculateCPATime(Vector3d firstPoint, Vector3d firstVelocity, Vector3d secondPoint, Vector3d secondVelocity)
        {
            Vector3d wNot = secondPoint - firstPoint;
            Vector3d relativeVelocity = secondVelocity - firstVelocity;
            return Vector3d.Dot(wNot, relativeVelocity) / relativeVelocity.LengthSquared;
        }
    }
}
