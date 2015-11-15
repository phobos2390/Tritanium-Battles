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
    public class PropelledObject :IModelObject
    {
        private static double FUELPERSEC = 1;
        double thrust;
        double fuel;
        IModelObject modObj;
        
        public PropelledObject(double thrust, double fuel, IModelObject obj)
        {
            this.fuel = fuel;
            this.thrust = thrust;
            modObj = obj;
        }

        private void expendFuel(FrameEventArgs e)
        {
            fuel -= FUELPERSEC * e.Time;
            if(fuel <= 0)
            {
                fuel = 0;
                thrust = 0;
            }
        }

        public void OnUpdate(FrameEventArgs e)
        {
            modObj.OnUpdate(e);
            expendFuel(e);
            Vector3d accelVector = Vector3d.UnitZ;
            accelVector = Vector3d.Transform(accelVector, Orientation);
            accelVector.NormalizeFast();
            accelVector = Vector3d.Multiply(accelVector, -thrust);
            this.AddForce(accelVector);
        }

        public void AddForce(Vector3d force)
        {
            modObj.AddForce(force);
        }

        public void Destroy()
        {
            modObj.Destroy();
        }

        public Vector3d Position
        {
            get
            {
                return modObj.Position;
            }
        }

        public double Mass
        {
            get
            {
                return modObj.Mass;
            }
        }

        public IList<Group> Groups
        {
            get
            {
                return modObj.Groups;
            }
        }

        public IList<Vertex> Vertices
        {
            get
            {
                return modObj.Vertices;
            }
        }

        Vector3d IModelObject.Position
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

        public bool EqualsOtherObject(IModelObject other)
        {
            return other.EqualsOtherObject(modObj);
        }
    }
}
