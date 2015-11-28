using LightGameEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Data.VertexData;
using OpenTK;
using LightGameEngine.Collision;

namespace LightGameEngine
{
    public class ComputerControlledOpponent : IModelObject, IAlignedShip, IComputerStateMachine
    {
        public const double TURNSPEED = 0.1;
        public const double RADSQUARE = 1;

        private ShipObject controlled;
        private IComputerState currentState;

        public void OnSeesObject(object sender, OnSightEventArgs e)
        {
            if(e.SeenObject is IAlignedShip)
            {
                IAlignedShip otherShip = (IAlignedShip) e.SeenObject;
                if(otherShip.ShipAlignment != controlled.ShipAlignment)
                {
                    currentState.OnSeesOpponent(e.SeenObject);
                }
            }
        }

        public void OnDeathOfShip(object sender, OnDeathEventArgs e)
        {

        }

        public ComputerControlledOpponent(ShipObject controlled, IComputerState currentState)
        {
            this.controlled = controlled;
            this.controlled.OnSight += OnSeesObject;
            OnDeath += OnDeathOfShip;
            this.currentState = currentState;
        }

        public bool Destroyed
        {
            get
            {
                return ((IModelObject)controlled).Destroyed;
            }
        }

        public IList<Group> Groups
        {
            get
            {
                return ((IModelObject)controlled).Groups;
            }
        }

        public double Mass
        {
            get
            {
                return ((IModelObject)controlled).Mass;
            }
        }

        public IList<Normal> Normals
        {
            get
            {
                return ((IModelObject)controlled).Normals;
            }
        }

        public Quaterniond Orientation
        {
            get
            {
                return ((IModelObject)controlled).Orientation;
            }

            set
            {
                ((IModelObject)controlled).Orientation = value;
            }
        }

        public Vector3d Position
        {
            get
            {
                return ((IModelObject)controlled).Position;
            }

            set
            {
                ((IModelObject)controlled).Position = value;
            }
        }

        public double RadiusSquared
        {
            get
            {
                return ((IModelObject)controlled).RadiusSquared;
            }
        }

        public Vector3d Velocity
        {
            get
            {
                return ((IModelObject)controlled).Velocity;
            }

            set
            {
                ((IModelObject)controlled).Velocity = value;
            }
        }

        public IList<Vertex> Vertices
        {
            get
            {
                return ((IModelObject)controlled).Vertices;
            }
        }

        Vector3d IComputerStateMachine.Position
        {
            get
            {
                return controlled.Position;
            }
        }

        public Alignment ShipAlignment
        {
            get
            {
                return ((IAlignedShip)controlled).ShipAlignment;
            }
        }

        public event OnDeathHandler OnDeath;

        public void AddForce(Vector3d force)
        {
            ((IModelObject)controlled).AddForce(force);
        }

        public void Destroy(IModelObject destroyer)
        {
            if (!Destroyed)
            {
                controlled.Destroy(destroyer);
                OnDeath(this, new OnDeathEventArgs(this, destroyer));
            }
        }

        public bool EqualsOtherObject(IModelObject other)
        {
            return ((IModelObject)controlled).EqualsOtherObject(other);
        }

        public void OnUpdate(FrameEventArgs e)
        {
            ((IModelObject)controlled).OnUpdate(e);
            this.currentState.OnUpdate(e);
        }

        void IComputerStateMachine.FireEngines()
        {
            controlled.FireEngines();
        }

        void IComputerStateMachine.FireWeapons()
        {
            controlled.FireWeapon();
            controlled.FireWeapon();
        }

        void IComputerStateMachine.TurnTowardsPoint(Vector3d point, double time)
        {
            Vector3d toLookTowards = point - Position;
            if (toLookTowards.LengthSquared > 0)
            {
                Vector3d currRay = Vector3d.Transform(-Vector3d.UnitZ, Orientation);
                Vector3d rotationAxis = Vector3d.Cross(currRay, toLookTowards);
                Sphere sphere = new Sphere(point, RadiusSquared);
                rotationAxis.NormalizeFast();
                Angle angleOfRotation = Angle.ArcCosine(Vector3d.Dot(currRay, toLookTowards), currRay.LengthFast * toLookTowards.LengthFast);
                Quaterniond lookingAtOrientation = Quaterniond.FromAxisAngle(rotationAxis, angleOfRotation.Radians);
                lookingAtOrientation = lookingAtOrientation * Orientation;
                Orientation = Quaterniond.Slerp(Orientation, lookingAtOrientation, Math.Min(TURNSPEED * time, 1));
                if(sphere.Intersects(Position, currRay).Item1)
                {
                    ((IComputerStateMachine)this).FireEngines();
                }
            }
        }

        void IComputerStateMachine.UpdateState(IComputerState newState)
        {
            this.currentState = newState;
        }
    }
}
