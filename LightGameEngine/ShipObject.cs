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
    public class ShipObject:IModelObject
    {
        private MissileArray complement;
        private IModelObject modObj;
        private static double FUELPERSEC = 1;
        private double thrust;
        private double fuel;
        private bool firingEngines;

        public ShipObject(double thrust, double fuel, MissileArray complement, IModelObject modObj)
        {
            this.thrust = thrust;
            this.fuel = fuel;
            this.complement = complement;
            this.modObj = modObj;
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

        public IList<Vertex> Vertices
        {
            get
            {
                return modObj.Vertices;
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

        public void FireWeapon()
        {
            this.complement.Fire();
        }

        public void FireEngines()
        {
            this.firingEngines = true;
            Console.WriteLine("Firing Engines");
        }

        private void expendFuel(FrameEventArgs e)
        {
            fuel -= FUELPERSEC * e.Time;
            if (fuel <= 0)
            {
                fuel = 0;
                thrust = 0;
            }
        }

        public void AddForce(Vector3d force)
        {
            modObj.AddForce(force);
        }

        public void OnUpdate(FrameEventArgs e)
        {
            if(this.firingEngines)
            {
                this.expendFuel(e);
                Vector3d zVec = Angle.ZVector(this.Pitch, this.Yaw, thrust);
                this.AddForce(zVec);
                this.firingEngines = false;
            }
            modObj.OnUpdate(e);
        }
    }
}
