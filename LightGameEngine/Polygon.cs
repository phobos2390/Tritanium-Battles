using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Data.VertexData;

namespace LightGameEngine.Collision
{
    public class Polygon:IIntersectableShape
    {
        private const double EPSILON = 0.1E-15;
        private IList<Vector3d> points;
        private Vector3d normal;
        private double distance;
        private Vector3d center;
        private double radiusSquare;

        public Polygon(IList<Vector3d> points)
        {
            this.points = points;
            this.normal = calculateNormal();
            this.distance = Vector3d.Dot(points[0], this.normal);
        }

        public Polygon(Face face, IList<Vertex> vertices)
        {
            this.points = new List<Vector3d>();
            for(int i = 0; i < face.Count; ++i)
            {
                Vertex vert = vertices[face[i].VertexIndex - 1];
                this.points.Add(new Vector3d(vert.X, vert.Y, vert.Z));
            }
            this.normal = calculateNormal();
            this.distance = Vector3d.Dot(points[0], this.normal);
        }

        private Vector3d calculateNormal()
        {
            Vector3d returnNormal = Vector3d.Zero;
            Vector3d total = Vector3d.Zero;
            bool colinear = true;
            for (int i = 0; i < points.Count; ++i)
            {
                total += points[i];
                if(colinear)
                {
                    Vector3d a = points[(i + 2) % points.Count] - points[(i + 1) % points.Count];
                    Vector3d b = points[i] - points[(i + 1) % points.Count];
                    Vector3d cross = Vector3d.Cross(a, b);
                    if (cross.LengthSquared > 0)
                    {
                        returnNormal = cross.Normalized();
                        colinear = false;
                    }
                }
            }
            if (!colinear)
            {
                this.center = Vector3d.Divide(total, points.Count);
                for (int i = 0; i < points.Count; ++i)
                {
                    Vector3d radius = points[i] - this.center;
                    double newLengthSquared = radius.LengthSquared;
                    if (newLengthSquared > this.radiusSquare)
                    {
                        this.radiusSquare = newLengthSquared;
                    }
                }
                return returnNormal;
            }
            else
            {
                throw new ArgumentException("Error. All points are Colinear. Plane is undefined");
            }
        }

        public Tuple<bool,Vector3d,Vector3d> Intersects(Vector3d initial, Vector3d ray)
        {
            bool rayIntersectsSphere = Sphere.IntersectSphere(initial, ray, this.center, this.radiusSquare);
            var result = IntersectsPlane(initial, ray);
            if (result.Item1 && rayIntersectsSphere)
            {
                Vector3d pTimesRayNormDot = IntersectionMultByRayNormDot(initial, ray, result.Item2);
                if (Sphere.PointInSphere(pTimesRayNormDot, Vector3d.Multiply(this.center, result.Item2), this.radiusSquare * result.Item2 * result.Item2))
                {
                    Vector3d intersect = Vector3d.Divide(pTimesRayNormDot, result.Item2);
                    if (Vector3d.Dot(intersect - initial, ray) > 0)
                    {
                        Vector3d outGoing = Vector3d.Cross(ray, normal);
                        int crossings = 0;
                        Vector3d lastPoint = Vector3d.Multiply(points[0], 1) - intersect;
                        if (outGoing == Vector3d.Zero)
                        {
                            outGoing = lastPoint;
                            int ctr = 1;
                            while (outGoing == Vector3d.Zero)
                            {
                                outGoing = Vector3d.Multiply(points[ctr++], 1) - intersect;
                            }
                        }
                        while (outGoing.LengthSquared < 4)
                        {
                            outGoing += outGoing;
                        }
                        Vector3d lastCross = Vector3d.Cross(outGoing, lastPoint);
                        double lastDot = Vector3d.Dot(outGoing, lastPoint);
                        double lastBelow = Vector3d.Dot(lastCross, normal);
                        bool lastNegative = lastBelow < 0;
                        for (int i = 0; i < points.Count; ++i)
                        {
                            Vector3d currPoint = points[(i + 1) % points.Count] - intersect;
                            Vector3d currCross = Vector3d.Cross(outGoing, currPoint);
                            double currDot = Vector3d.Dot(outGoing, currPoint);
                            double currBelow = Vector3d.Dot(currCross, normal);
                            bool currNegative = currBelow < 0;
                            if (currNegative != lastNegative)
                            {
                                if (currDot > 0 && lastDot > 0)
                                {
                                    ++crossings;
                                }
                                else if (currNegative && Vector3d.Dot(Vector3d.Cross(currPoint - lastPoint, currPoint), normal) > 0)
                                {
                                    ++crossings;
                                }
                                else if (lastNegative && Vector3d.Dot(Vector3d.Cross(currPoint, currPoint - lastPoint), normal) > 0)
                                {
                                    ++crossings;
                                }
                            }
                            lastNegative = currNegative;
                            lastPoint = currPoint;
                            lastDot = currDot;
                        }
                        bool hit = crossings % 2 == 1;
                        if (hit)
                        {
                            return Tuple.Create(hit, intersect, this.normal);
                        }
                    }
                }
            }
            return Tuple.Create(false, Vector3d.Zero, Vector3d.Zero);
        }

        public Tuple<bool, double> IntersectsPlane(Vector3d initial, Vector3d ray)
        {
            double rayNormDot = Vector3d.Dot(ray, this.normal);
            return Tuple.Create(Math.Abs(rayNormDot) > 0, rayNormDot);
        }

        public Vector3d IntersectionMultByRayNormDot(Vector3d initial, Vector3d ray, double rayNormDot)
        {
            return Vector3d.Multiply(ray, this.distance - Vector3d.Dot(initial, this.normal)) + Vector3d.Multiply(initial,rayNormDot);
        }

        public Vector3d Normal
        {
            get
            {
                return this.normal;
            }
        }
    }
}
