﻿using System;
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

        public IList<Normal> Normals
        {
            get
            {
                return modObj.Normals;
            }
        }

        public bool Destroyed
        {
            get
            {
                return modObj.Destroyed;
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

            bool explode = false;

            foreach (IModelObject obj in this.model.Objects)
            {
                if (!this.EqualsOtherObject(obj) && !this.firedBy.EqualsOtherObject(obj))
                {
                    Vector3d displacement = this.Position - initial;
                    Vector3d dist = this.Position - obj.Position;
                    Vector3d initDist = initial - obj.Position;
                    double distDispDot = Vector3d.Dot(dist, displacement);
                    double radiusSquared = this.blastRadius * this.blastRadius;
                    double distLengthSquare = dist.LengthSquared;
                    double displacementLengthSquare = displacement.LengthSquared;
                    if (distLengthSquare <= radiusSquared)
                    {
                        Console.WriteLine("1Destroying object");
                        obj.Destroy();
                        explode = true;
                    }
                    else if (Vector3d.Dot(initDist, displacement) <= 0 && distDispDot >= 0 && displacementLengthSquare > 0)
                    {
                        Console.WriteLine("Might have passed it");
                        Vector3d rejectionProb = Vector3d.Multiply(displacement, distDispDot)
                            - Vector3d.Multiply(dist, displacementLengthSquare);
                        double dl4 = displacementLengthSquare * displacementLengthSquare;
                        if (radiusSquared * dl4 >= rejectionProb.LengthSquared)
                        {

                            Console.WriteLine("2Destroying object");
                            obj.Destroy();
                            explode = true;
                        }
                    }
                }
            }
            if(explode)
            {
                this.Destroy();
            }
        }

        public void Destroy()
        {
            modObj.Destroy();
        }
        
        public bool EqualsOtherObject(IModelObject other)
        {
            return other.EqualsOtherObject(modObj);
        }
    }
}
