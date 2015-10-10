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
    public class Missile:IModelObject
    {
        private IModelObject modObj;
        private Model model;
        private double blastRadius;

        public Missile(double blastRadius, Model model, IModelObject modObj)
        {
            this.model = model;
            this.modObj = modObj;
            this.blastRadius = blastRadius;
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
        }

        public Vector3d Position
        {
            get
            {
                return modObj.Position;
            }
        }

        public Angle Roll
        {
            get
            {
                return modObj.Roll;
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
        }

        public void AddForce(Vector3d force)
        {
            modObj.AddForce(force);
        }

        public void OnUpdate(FrameEventArgs e)
        {
            modObj.OnUpdate(e);

            foreach(IModelObject obj in this.model.Objects)
            {
                if(obj != this)
                {
                    Vector3d dist = this.Position - obj.Position;
                    if(dist.LengthSquared <= this.blastRadius * this.blastRadius)
                    {
                        model.DestroyObject(obj);
                    }
                }
            }
        }
    }
}
