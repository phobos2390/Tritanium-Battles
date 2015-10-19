using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Data.VertexData;
using OpenTK;

namespace LightGameEngine.Model
{
    public class Missile:IModelObject
    {
        private IModelObject modObj;
        private IModelObject firedBy;
        private Model model;
        private double blastRadius;

        public Missile(double blastRadius, Model model, IModelObject firedBy, IModelObject modObj)
        {
            this.model = model;
            this.modObj = modObj;
            this.firedBy = firedBy;
            this.Pitch = Angle.CreateDegree(firedBy.Pitch.Degrees);
            this.Yaw = Angle.CreateDegree(firedBy.Yaw.Degrees);
            this.Position = firedBy.Position;
            this.blastRadius = blastRadius;
        }

        public IList<Group> Groups
        {
            get
            {
                return modObj.Groups;
            }
        }

        public double Mass
        {
            get
            {
                return modObj.Mass;
            }
        }
        
        public IList<Vertex> Vertices
        {
            get
            {
                return modObj.Vertices;
            }
        }

        public Vector3d Position
        {
            get
            {
                return modObj.Position;
            }

            set
            {
                modObj.Position = value;
            }
        }

        public Angle Pitch
        {
            get
            {
                return modObj.Pitch;
            }

            set
            {
                modObj.Pitch = value;
            }
        }

        public Angle Yaw
        {
            get
            {
                return modObj.Yaw;
            }

            set
            {
                modObj.Yaw = value;
            }
        }

        public Angle Roll
        {
            get
            {
                return modObj.Roll;
            }

            set
            {
                modObj.Roll = value;
            }
        }

        public void AddForce(Vector3d force)
        {
            modObj.AddForce(force);
        }

        public void OnUpdate(FrameEventArgs e)
        {
            Vector3d initial = this.Position;

            modObj.OnUpdate(e);

            foreach (IModelObject obj in this.model.Objects)
            {
                if (obj != this && obj != this.firedBy)
                {
                    Vector3d displacement = this.Position - initial;
                    Vector3d dist = this.Position - obj.Position;
                    if (dist.LengthSquared <= displacement.LengthSquared)
                    {
                        Vector3d initDist = initial - obj.Position;
                        double distDispDot = Vector3d.Dot(dist, displacement);
                        double radiusSquared = this.blastRadius * this.blastRadius;
                        double distLengthSquare = dist.LengthSquared;
                        double displacementLengthSquare = displacement.LengthSquared;
                        if (distLengthSquare <= radiusSquared)
                        {
                            model.DestroyObject(obj);
                            model.DestroyObject(this);
                        }
                        else if (Vector3d.Dot(initDist, displacement) <= 0 && distDispDot >= 0)
                        {
                            Vector3d rejectionProb = distDispDot * displacement - dist * displacementLengthSquare;
                            double radius4 = radiusSquared * radiusSquared;
                            double dl4 = displacementLengthSquare * displacementLengthSquare;
                            if (radius4 * dl4 <= rejectionProb.LengthSquared)
                            {
                                model.DestroyObject(obj);
                                model.DestroyObject(this);
                            }
                        }
                    }
                }
            }
        }
    }
}
