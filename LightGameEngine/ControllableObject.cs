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
    public class ControllableObject:IModelObject,IGamepadInterface
    {
        private static double ANGLE_MOVE = 1;
        private ShipObject modObj;
        private bool firingEngines;

        public ControllableObject(ShipObject obj)
        {
            this.modObj = obj;
            this.firingEngines = false;
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
                return ((IModelObject)modObj).Normals;
            }
        }

        public bool Destroyed
        {
            get
            {
                return ((IModelObject)modObj).Destroyed;
            }
        }

        public Quaterniond Orientation
        {
            get
            {
                return ((IModelObject)modObj).Orientation;
            }

            set
            {
                ((IModelObject)modObj).Orientation = value;
            }
        }

        public Vector3d Velocity
        {
            get
            {
                return ((IModelObject)modObj).Velocity;
            }

            set
            {
                ((IModelObject)modObj).Velocity = value;
            }
        }

        public void AddForce(Vector3d force)
        {
            modObj.AddForce(force);
        }

        private bool quatNaN(Quaterniond quat)
        {
            bool xValid = double.IsNaN(quat.X);
            bool yValid = double.IsNaN(quat.Y);
            bool zValid = double.IsNaN(quat.Z);
            bool wValid = double.IsNaN(quat.W);
            return xValid || yValid || zValid || wValid;
        }

        public void MoveLeftJoystick(Vector2d position)
        {
            Angle addRoll = Angle.CreateDegree(-ANGLE_MOVE * position.X);
            Angle addYaw = Angle.CreateDegree(-ANGLE_MOVE * position.Y);
            Quaterniond xRot = Quaterniond.FromAxisAngle(Vector3d.UnitX, addYaw.Radians);
            Quaterniond zRot = Quaterniond.FromAxisAngle(Vector3d.UnitZ, addRoll.Radians);
            Orientation = (Orientation * xRot * zRot).Normalized();
        }

        public void MoveRightJoystick(Vector2d position)
        {
            Angle addRoll = Angle.CreateDegree(-ANGLE_MOVE * position.X);
            Angle addYaw = Angle.CreateDegree(-ANGLE_MOVE * position.Y);
            Quaterniond xRot = Quaterniond.FromAxisAngle(Vector3d.UnitX, addYaw.Radians);
            Quaterniond zRot = Quaterniond.FromAxisAngle(Vector3d.UnitY, addRoll.Radians);
            Orientation = (Orientation * xRot * zRot).Normalized();
        }

        public void OnUpdate(FrameEventArgs e)
        {
            if(this.firingEngines)
            {
                modObj.FireEngines();
            }
            modObj.OnUpdate(e);
        }

        public void PressA()
        {
            this.modObj.FireWeapon();
        }

        public void PressB()
        {
            this.firingEngines = true;
        }

        public void PressDDown()
        {
        }

        public void PressDLeft()
        {
        }

        public void PressDRight()
        {
        }

        public void PressDUp()
        {
        }

        public void PressShoulderLeft()
        {
        }

        public void PressShoulderRight()
        {
        }

        public void PressX()
        {
            this.modObj.SwapMissileType();
        }

        public void PressY()
        {
        }

        public void ReleaseA()
        {
        }

        public void ReleaseB()
        {
            this.firingEngines = false;
        }

        public void ReleaseDDown()
        {
        }

        public void ReleaseDLeft()
        {
        }

        public void ReleaseDRight()
        {
        }

        public void ReleaseDUp()
        {
        }

        public void ReleaseShoulderLeft()
        {
        }

        public void ReleaseShoulderRight()
        {
        }

        public void ReleaseX()
        {
        }

        public void ReleaseY()
        {
        }

        public void Destroy()
        {
            ((IModelObject)modObj).Destroy();
        }

        public bool EqualsOtherObject(IModelObject other)
        {
            return other.EqualsOtherObject(modObj);
        }
    }
}
