using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryLibrary
{
    public class FieldOfView
    {
        Vector3d viewRay;
        //the calculations use d/h for the cosine value over an angle as it makes for a slightly more precise calculation
        double hypotenuse;
        double hypotenuseSquared;
        double distance;
        double distanceSquared;

        private FieldOfView(Vector3d viewRay, double hypotenuse, double hypotenuseSquared, double distance, double distanceSquared)
        {
            this.viewRay = viewRay.Normalized();
            this.hypotenuse = hypotenuse;
            this.hypotenuseSquared = hypotenuseSquared;
            this.distance = distance;
            this.distanceSquared = distanceSquared;
        }

        public static FieldOfView FromHypotenuseandDistance(Vector3d viewRay, double hyp, double dist)
        {
            return new FieldOfView(viewRay, hyp, hyp * hyp, dist, dist * dist);
        }

        public static FieldOfView FromHypotenuseSquaredandDistanceSquared(Vector3d viewRay, double hypSqr, double distSqr)
        {
            return new FieldOfView(viewRay, Math.Sqrt(hypSqr), hypSqr, Math.Sqrt(distSqr), distSqr);
        }

        public static FieldOfView FromDistanceandError(Vector3d viewRay, double dist, double err)
        {
            return FromHypotenuseandDistance(viewRay, dist + err, dist);
        }

        public static FieldOfView FromAngle(Vector3d viewRay, Angle angle)
        {
            return FromHypotenuseandDistance(viewRay, 1, angle.Cosine());
        }

        public bool Viewable(Vector3d relativePosition)
        {
            double magnitudeSquared = relativePosition.LengthSquared;
            double dotProduct = Vector3d.Dot(relativePosition, viewRay);
            dotProduct *= dotProduct;
            return hypotenuseSquared * dotProduct <= distanceSquared * magnitudeSquared;
        }
    }
}
