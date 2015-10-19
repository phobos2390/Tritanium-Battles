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

        public void AddForce(Vector3d force)
        {
            modObj.AddForce(force);
        }

        public void MoveLeftJoystick(Vector2d position)
        {
            //Console.WriteLine("Rotating Ship ({0},{1})", ANGLE_MOVE * position.X, ANGLE_MOVE * position.Y);
            Angle newRoll = Roll;
            newRoll.AddAngle(Angle.CreateDegree(ANGLE_MOVE*position.X));
            Roll = newRoll;
            Angle newYaw = Yaw;
            newYaw.AddAngle(Angle.CreateDegree(-ANGLE_MOVE * position.Y));
            Yaw = newYaw;
            //Console.WriteLine("Ship Orientation ({0},{1})", Roll.Degrees, Yaw.Degrees);
        }

        public void MoveRightJoystick(Vector2d position)
        {
            Angle newPitch = Pitch;
            newPitch.AddAngle(Angle.CreateDegree(ANGLE_MOVE * position.X));
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
            throw new NotImplementedException();
        }

        public void ReleaseDLeft()
        {
            throw new NotImplementedException();
        }

        public void ReleaseDRight()
        {
            throw new NotImplementedException();
        }

        public void ReleaseDUp()
        {
            throw new NotImplementedException();
        }

        public void ReleaseShoulderLeft()
        {
            throw new NotImplementedException();
        }

        public void ReleaseShoulderRight()
        {
            throw new NotImplementedException();
        }

        public void ReleaseX()
        {
            throw new NotImplementedException();
        }

        public void ReleaseY()
        {
            throw new NotImplementedException();
        }
    }
}
