using GeometryLibrary;
using LightGameEngine.Model;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Controller
{
    public class PhysicsController
    {
        static double GRAVITATIONAL_CONST = 6.67408E-11;
        private Model.Model model;

        public PhysicsController(Model.Model model)
        {
            this.model = model;
        }

        public void OnUpdateFrame(FrameEventArgs e)
        {
            model.OnUpdate(e);

            //foreach(Model.IModelObject modObj in model.Objects)
            //{
            //    foreach (Model.IModelObject modObj2 in model.Objects)
            //    {
            //        if(modObj != modObj2)
            //        {
            //            Vector3d displacement = modObj2.Position - modObj.Position;
            //            double r = displacement.LengthSquared;
            //            double m1m2 = modObj.Mass * modObj2.Mass;
            //            displacement.Normalize();
            //            displacement = Vector3d.Multiply(displacement, GRAVITATIONAL_CONST * m1m2 / r);
            //            modObj.AddForce(displacement);
            //        }
            //    }
            //}
            for (int i = 0; i < model.Objects.Count - 1; ++i)
            {
                for(int j = i + 1; j < model.Objects.Count; ++j)
                {
                    IModelObject firstObject = model.Objects[i];
                    IModelObject secondObject = model.Objects[j];
                    Vector3d diffPosition = firstObject.Position - secondObject.Position;
                    Vector3d diffVelocity = firstObject.Velocity - secondObject.Velocity;
                    Vector3d diffApprox = diffVelocity + diffPosition;
                    if(!firstObject.Destroyed && !secondObject.Destroyed && diffApprox.LengthSquared < firstObject.RadiusSquared + 2 * firstObject.RadiusSquared * secondObject.RadiusSquared + secondObject.RadiusSquared)
                    {
                        double cpaSqr = Intersector.CalculateCPASqr(firstObject.Position, firstObject.Velocity, secondObject.Position, secondObject.Velocity);
                        double r1Sqr = firstObject.RadiusSquared;
                        double r2Sqr = secondObject.RadiusSquared;
                        double r1 = Math.Sqrt(r1Sqr);
                        double r2 = Math.Sqrt(r2Sqr);
                        if (firstObject is Missile || secondObject is Missile)
                        {
                            Missile theMissile;
                            IModelObject target;
                            if (firstObject is Missile)
                            {
                                theMissile = firstObject as Missile;
                                target = secondObject;
                            }
                            else
                            {
                                target = firstObject;
                                theMissile = secondObject as Missile;
                            }
                            double mr = theMissile.BlastRadius;
                            double trSqr = target.RadiusSquared;
                            double tr = Math.Sqrt(trSqr);
                            double mrSqr = mr * mr;
                            double radSqr = mrSqr + 2 * mr * tr + trSqr;
                            if (radSqr >= cpaSqr && !theMissile.FiredBy.EqualsOtherObject(target))
                            {
                                Console.WriteLine("Missile Hit another Object");
                                theMissile.Destroy(theMissile);
                                target.Destroy(theMissile);
                            }
                        }
                        else
                        {
                            double radiusSquared = r1Sqr + 2 * r1 * r2 + r2Sqr;
                            if (radiusSquared >= cpaSqr)
                            {
                                if (!(firstObject is Explosion))
                                {
                                    firstObject.Destroy(secondObject);
                                    Console.WriteLine("Object " + firstObject.ToString() + "was destroyed");
                                }
                                if (!(secondObject is Explosion))
                                {
                                    secondObject.Destroy(firstObject);
                                    Console.WriteLine("Object " + secondObject.ToString() + " Destroyed");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
