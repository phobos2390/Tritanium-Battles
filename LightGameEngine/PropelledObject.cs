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
            Vector3d accelVector = Angle.ZVector(Pitch, Yaw, thrust);

            modObj.AddForce(accelVector);
        }

        public void AddForce(Vector3d force)
        {
            modObj.AddForce(force);
        }

        public Vector3d Position
        {
            get
            {
                return modObj.Position;
            }
        }

        public Angle Pitch
        {
            get
            {
                return modObj.Pitch;
            }
        }

        public Angle Yaw
        {
            get
            {
                return modObj.Yaw;
            }
        }

        public Angle Roll
        {
            get
            {
                return modObj.Roll;
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

        Angle IModelObject.Pitch
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

        Angle IModelObject.Yaw
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

        Angle IModelObject.Roll
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
    }
}
