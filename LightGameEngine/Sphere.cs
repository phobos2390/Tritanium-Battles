using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Collision
{
    public class Sphere : IIntersectableShape
    {
        private Vector3d center;
        private double radiusSquared;

        public Sphere(Vector3d center, double radiusSquared)
        {
            this.center = center;
            this.radiusSquared = radiusSquared;
        }

        public Tuple<bool,Vector3d, double,double,double> IntersectionVals(Vector3d initial, Vector3d ray)
        {
            Vector3d c = center - initial;
            double crossLengthSquared = Vector3d.Cross(ray, c).LengthSquared;
            bool intersects = false;
            if(crossLengthSquared <= radiusSquared * ray.LengthSquared && Vector3d.Dot(c, ray) >= 0)
            {
                intersects = true;
            }
            return Tuple.Create(intersects, c, radiusSquared, crossLengthSquared, ray.LengthSquared);
        }

        public static bool IntersectSphere(Vector3d initial, Vector3d ray, Vector3d center, double radiusSquared)
        {
            Vector3d c = center - initial;
            return Vector3d.Cross(ray, c).LengthSquared <= radiusSquared * ray.LengthSquared && Vector3d.Dot(c, ray) >= 0;
        }

        public static bool PointInSphere(Vector3d point, Vector3d center, double radiusSquared)
        {
            return (point - center).LengthSquared <= radiusSquared;
        }
        
        public Tuple<bool, Vector3d, Vector3d> Intersects(Vector3d initial, Vector3d ray)
        {
            var result = IntersectionVals(initial, ray);
            if(result.Item1)
            {
                Vector3d intersection = Intersection(initial, result.Item2, ray, result.Item3, result.Item4, result.Item5);
                return Tuple.Create<bool, Vector3d, Vector3d>(result.Item1, intersection, Normal(intersection));
            }
            else
            {
                return Tuple.Create<bool, Vector3d, Vector3d>(result.Item1, Vector3d.Zero, Vector3d.Zero);
            }
        }

        public Vector3d Intersection(Vector3d initial, Vector3d c, Vector3d ray, double radiusSquared, double dNom, double dDen)
        {
            return Vector3d.Multiply(ray, Vector3d.Dot(c, ray)/ray.LengthSquared - Math.Sqrt(radiusSquared - dNom / dDen)) + initial;
        }

        public Vector3d Normal(Vector3d intersection)
        {
            return Vector3d.Divide(intersection - center, Math.Sqrt(this.radiusSquared));
        }

        public Vector3d FindRefractionIntersection(Vector3d intersection,Vector3d ray)
        {
            Vector3d c = this.center - intersection;
            Vector3d retVec = Vector3d.Multiply(ray, 2 * Vector3d.Dot(c, ray)) + Vector3d.Multiply(intersection, ray.LengthSquared);
            return Vector3d.Divide(retVec, ray.LengthSquared);
        }
    }
}
