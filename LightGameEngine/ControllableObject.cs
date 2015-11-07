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

        public void AddForce(Vector3d force)
        {
            modObj.AddForce(force);
        }

        public void MoveLeftJoystick(Vector2d position)
        {
            Angle addRoll = Angle.CreateDegree(-ANGLE_MOVE * position.X);
            Angle newRoll = Roll;
            newRoll.AddAngle(addRoll);
            Roll = newRoll;
            Angle curYaw = Angle.CreateDegree(-ANGLE_MOVE * position.Y);
            if(Math.Abs(position.Y) > 0.01525)
            {
                Vector3d zVec = Angle.ZVector(Pitch, Yaw);
                Matrix4d rRoll = Matrix4d.CreateRotationZ(-Roll.Radians);
                Matrix4d yaw = Matrix4d.CreateRotationX(curYaw.Radians);
                Matrix4d revRRoll = Matrix4d.CreateRotationZ(Roll.Radians);
                zVec = Vector3d.Transform(zVec, revRRoll * yaw * rRoll);
                //zVec = Vector3d.Transform(zVec, yaw);
                var angles = Angle.AngleOfVector(zVec);
                Pitch = angles.Item1;
                Yaw = angles.Item2;
            }
            //Angle addPitch = Angle.CreateDegree(angleScale * Roll.Sine());
            //Angle addYaw = Angle.CreateDegree(angleScale * Roll.Cosine());
            //Angle newPitch = Pitch;
            //newPitch.AddAngle(addPitch);
            //Pitch = newPitch;
            //Angle newYaw = Yaw;
            //newYaw.AddAngle(addYaw);
            //Yaw = newYaw;
        }

        public void MoveRightJoystick(Vector2d position)
        {
            Angle newPitch = Pitch;
            newPitch.AddAngle(Angle.CreateDegree(-ANGLE_MOVE * position.X));
            Pitch = newPitch;
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
