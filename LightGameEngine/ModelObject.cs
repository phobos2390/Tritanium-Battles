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
        private static int currIndex = 0;

        private double mass;
        private Vector3d netForce;
        private Quaterniond orientation;
        private Vector3d position;
        private Vector3d velocity;
        private IList<Group> groups;
        private IList<Normal> normals;
        private IList<Vertex> vertices;
        private IList<Texture> textures;
        private IList<Material> materials;
        private bool destroyed;
        private int index;

        public ModelObject(LoadResult result, double mass, Quaterniond orientation, Vector3d position)
        {
            this.groups = result.Groups;
            this.normals = result.Normals;
            this.vertices = result.Vertices;
            this.textures = result.Textures;
            this.materials = result.Materials;
            this.mass = mass;
            this.orientation = orientation;
            this.position = position;
            this.velocity = Vector3d.Zero;
            this.destroyed = false;
            this.index = currIndex++;
        }

        public ModelObject(LoadResult result, double mass)
            :this(result, mass, Quaterniond.Identity,Vector3d.Zero) {}

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

        public void Destroy()
        {
            if(!this.destroyed)
            {
                this.destroyed = true;
            }
        }

        private bool quatNaN(Quaterniond quat)
        {
            bool xValid = double.IsNaN(quat.X);
            bool yValid = double.IsNaN(quat.Y);
            bool zValid = double.IsNaN(quat.Z);
            bool wValid = double.IsNaN(quat.W);
            return xValid || yValid || zValid || wValid;
        }

        public Quaterniond Orientation
        {
            get
            {
                return this.orientation;
            }
            set
            {
                this.orientation = value;
            }
        }

        public Vector3d Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
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

        public bool Destroyed
        {
            get
            {
                return this.destroyed;
            }
        }

        public bool EqualsOtherObject(IModelObject obj)
        {
            if(obj is ModelObject)
            {
                return ((ModelObject)obj).index == this.index;
            }
            else
            {
                return obj.EqualsOtherObject(this);
            }
        }
    }
}
