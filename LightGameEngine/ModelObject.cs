using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Data.VertexData;
using ObjLoader.Loader.Data;
using ObjLoader.Loader.Loaders;
using OpenTK;

namespace LightGameEngine.Model
{
    public class ModelObject : IModelObject
    {
        private double mass;
        private Vector3d netForce;
        private Angle xRotation;
        private Angle yRotation;
        private Angle zRotation;
        private Vector3d position;
        private Vector3d velocity;
        private IList<Group> groups;
        private IList<Normal> normals;
        private IList<Vertex> vertices;
        private IList<Texture> textures;
        private IList<Material> materials;

        public ModelObject(LoadResult result, double mass)
        {
            this.groups = result.Groups;
            this.normals = result.Normals;
            this.vertices = result.Vertices;
            this.textures = result.Textures;
            this.materials = result.Materials;
            this.mass = mass;
        }

        public ModelObject(LoadResult result)
            : this(result, 1)
        { }

        public double Mass
        {
            get
            {
                return this.mass;
            }
        }

        protected Vector3d Orientation
        {
            get
            {
                return Angle.ZVector(Pitch, Yaw);
            }
        }

        public virtual void OnUpdate(FrameEventArgs e)
        {
            velocity += netForce / mass * e.Time;
            position += velocity * e.Time;
            netForce = Vector3d.Zero;
        }

        public void AddForce(Vector3d force)
        {
            netForce += force;
        }

        public Angle Yaw
        {
            get
            {
                return this.xRotation;
            }
            set
            {
                this.xRotation = value;
            }
        }

        public Angle Pitch
        {
            get
            {
                return this.yRotation;
            }
            set
            {
                this.yRotation = value;
            }
        }

        public Angle Roll
        {
            get
            {
                return this.zRotation;
            }
            set
            {
                this.zRotation = value;
            }
        }

        public Vector3d Position
        {
            get
            {
                return this.position;
            }
        }

        public IList<Group> Groups
        {
            get
            {
                return this.groups;
            }
        }

        public IList<Normal> Normals
        {
            get
            {
                return this.normals;
            }
        }

        public IList<Vertex> Vertices
        {
            get
            {
                return this.vertices;
            }
        }

        public IList<Texture> Textures
        {
            get
            {
                return this.textures;
            }
        }

        public IList<Material> Materials
        {
            get
            {
                return this.materials;
            }
        }
    }
}
