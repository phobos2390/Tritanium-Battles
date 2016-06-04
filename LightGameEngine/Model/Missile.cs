using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Data.VertexData;
using OpenTK;
using LightGameEngine.Collision;

namespace LightGameEngine.Model
{
    public class Missile:IModelObject
    {
        private const double TIMEDELAY = 20;

        private IModelObject modObj;
        private IModelObject firedBy;
        private IModelObject explObj;
        private Model model;
        private double blastRadius;
        private double radiusSquare;
        private double timeLeft;

        public event OnDeathHandler OnDeath;

        public void OnExploded(object sender, OnDeathEventArgs e)
        {
            Console.WriteLine("Exploded");
        }

        public Missile(double blastRadius, Model model, Vector3d initialPosition, IModelObject firedBy, IModelObject modObj, IModelObject explosion)
        {
            this.model = model;
            this.modObj = modObj;
            this.firedBy = firedBy;
            this.Velocity = firedBy.Velocity;
            this.Orientation = firedBy.Orientation;
            this.Position = initialPosition;
            this.blastRadius = blastRadius;
            this.radiusSquare = this.blastRadius * this.blastRadius;
            explObj = explosion;
            timeLeft = TIMEDELAY;
            OnDeath += OnExploded;
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

        public Quaterniond Orientation
        {
            get
            {
                return modObj.Orientation;
            }

            set
            {
                modObj.Orientation = value;
            }
        }

        public Vector3d Velocity
        {
            get
            {
                return modObj.Velocity;
            }

            set
            {
                modObj.Velocity = value;
            }
        }

        public double RadiusSquared
        {
            get
            {
                return modObj.RadiusSquared;
            }
        }

        public double Scale
        {
            get
            {
                return modObj.Scale;
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
                if (!this.EqualsOtherObject(obj) && !this.firedBy.EqualsOtherObject(obj) && !(obj is Missile))
                {
                    Vector3d ray = obj.Position - initial;
                    Vector3d dist = this.Position - initial;
                    Vector3d revDist = this.Position - obj.Position;
                    if(Sphere.PointInSphere(obj.Position, this.Position, this.radiusSquare))
                    {
                        Console.WriteLine("1Destroying object");
                        obj.Destroy(this);
                        explode = true;
                    }
                    else if (Vector3d.Dot(ray, dist) > 0 && Vector3d.Dot(revDist, dist) > 0)
                    {
                        Console.WriteLine("Might have passed it");
                        double vxdLength = Vector3d.Cross(ray, dist).LengthSquared;
                        if (vxdLength <= this.radiusSquare * dist.LengthSquared)
                        {
                            Console.WriteLine("2Destroying object");
                            obj.Destroy(this);
                            explode = true;
                        }
                    }
                }
            }
            if(explode)
            {
                Destroy(this);
            }
            timeLeft -= e.Time;
            if(timeLeft <= 0)
            {
                Destroy(this);
            }
        }
        
        public bool EqualsOtherObject(IModelObject other)
        {
            return other.EqualsOtherObject(modObj);
        }

        public void Destroy(IModelObject destroyer)
        {
            if(!Destroyed)
            {
                modObj.Destroy(destroyer);
                OnDeath(this, new OnDeathEventArgs(this, destroyer));
                explObj.Position = Position;
                explObj.Velocity = Velocity;
                model.AddModelObject(explObj);
            }
        }
    }
}
