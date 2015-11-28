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
    public delegate void OnSightHandler(object sender, OnSightEventArgs e);

    public class ShipObject:IModelObject, IAlignedShip
    {
        public event OnSightHandler OnSight;

        private Alignment alignment;
        private IList<MissileArray> complement;
        private int currentFireMode;
        private int missileType;
        private IModelObject modObj;
        private static double FUELPERSEC = 1;
        private double thrust;
        private double fuel;
        private bool firingEngines;
        private Model model;

        public event OnDeathHandler OnDeath;

        public ShipObject(double thrust, double fuel, IList<MissileArray> complement, IModelObject modObj, Model model, Alignment alignment)
        {
            currentFireMode = 0;
            missileType = 0;
            this.thrust = thrust;
            this.fuel = fuel;
            this.complement = complement;
            this.modObj = modObj;
            this.model = model;
            this.alignment = alignment;
        }

        public Alignment ShipAlignment
        {
            get
            {
                return this.alignment;
            }
        }

        public int LeftMissiles
        {
            get
            {
                return complement[1].Count;
            }
        }

        public int RightMissiles
        {
            get
            {
                return complement[0].Count;
            }
        }

        public int LeftHighMissiles
        {
            get
            {
                return complement[3].Count;
            }
        }

        public int RightHighMissiles
        {
            get
            {
                return complement[2].Count;
            }
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

        public IList<Vertex> Vertices
        {
            get
            {
                return modObj.Vertices;
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

        public int MissileType
        {
            get
            {
                return this.missileType;
            }
        }

        public int FireMode
        {
            get
            {
                return this.currentFireMode;
            }
        }

        public double RadiusSquared
        {
            get
            {
                return modObj.RadiusSquared;
            }
        }

        public void SwapMissileType()
        {
            missileType = (missileType + 1) % 2;
        }

        public void FireWeapon()
        {
            int curIndex = this.missileType * 2 + this.currentFireMode;
            currentFireMode = (currentFireMode + 1) % 2;
            this.complement[curIndex].Fire();
        }

        public void FireEngines()
        {
            this.firingEngines = true;
            //Console.WriteLine("Firing Engines");
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

        private void seeingOtherObject(IModelObject obj)
        {
            OnSight(this, new OnSightEventArgs(obj));
        }

        public void OnUpdate(FrameEventArgs e)
        {
            Vector3d accelVector = -Vector3d.UnitZ;
            accelVector = Vector3d.Transform(accelVector, Orientation);
            var intersected = model.IntersectScene(Position, accelVector, this);
            if(intersected.Item1 != null)
            {
                seeingOtherObject(intersected.Item1);
            }
            if (this.firingEngines)
            {
                this.expendFuel(e);
                accelVector.NormalizeFast();
                accelVector = Vector3d.Multiply(accelVector, thrust);
                this.AddForce(accelVector);
                this.firingEngines = false;
            }
            modObj.OnUpdate(e);
        }

        public void Destroy(IModelObject destroyer)
        {
            modObj.Destroy(destroyer);
        }

        public bool EqualsOtherObject(IModelObject other)
        {
            return other.EqualsOtherObject(modObj);
        }
    }
}
