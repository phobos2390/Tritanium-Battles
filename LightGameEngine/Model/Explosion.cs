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
    public class Explosion:IModelObject
    {
        IModelObject modobj;
        int currentStage;
        int stages;
        double scale;
        double scaleVelocity;
        double scaleAcceleration;

        public event OnDeathHandler OnDeath;

        public Explosion(IModelObject obj, double acceleration, int duration)
        {
            currentStage = 0;
            stages = duration;
            modobj = obj;
            scale = 1;
            scaleVelocity = 0;
            scaleAcceleration = acceleration;
        }

        public bool Destroyed
        {
            get
            {
                if(currentStage >= stages)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public double Mass
        {
            get
            {
                return modobj.Mass;
            }
        }

        public double Scale
        {
            get
            {
                return modobj.Scale;
            }
        }

        public Vector3d Position
        {
            get
            {
                return modobj.Position;
            }

            set
            {
                modobj.Position = value;
            }
        }

        public Vector3d Velocity
        {
            get
            {
                return modobj.Velocity;
            }

            set
            {
                modobj.Velocity = value;
            }
        }

        public Quaterniond Orientation
        {
            get
            {
                return modobj.Orientation;
            }

            set
            {
                modobj.Orientation = value;
            }
        }

        public IList<Group> Groups
        {
            get
            {
                return modobj.Groups;
            }
        }

        public IList<Vertex> Vertices
        {
            get
            {
                return modobj.Vertices;
            }
        }

        public IList<Normal> Normals
        {
            get
            {
                return modobj.Normals;
            }
        }

        public double RadiusSquared
        {
            get
            {
                return scale * scale;
            }
        }

        public void AddForce(Vector3d force){}

        public void Destroy(IModelObject destroyer){}

        public bool EqualsOtherObject(IModelObject other)
        {
            return false;
        }

        public void OnUpdate(FrameEventArgs e)
        {
            modobj.OnUpdate(e);
            ++currentStage;
            scale += scaleVelocity * e.Time + .5 * scaleAcceleration * e.Time * e.Time;
            scaleVelocity += scaleAcceleration * e.Time;
            //Console.WriteLine("Current State: {Velocity:(" + Velocity.X + "," + Velocity.Y + "," + Velocity.Z + "), Position:(" + Position.X + "," + Position.Y + "," + Position.Z + "), Scale: " + Scale + ",Radius Squared: " + RadiusSquared + "}");
        }
    }
}
