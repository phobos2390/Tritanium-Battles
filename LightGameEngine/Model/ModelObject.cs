using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using ObjLoader.Loader.Loaders;
using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Data.VertexData;
using ObjLoader.Loader.Data;

namespace LightGameEngine.Model
{
    public class ModelObject : IModelObject
    {
        public event OnDeathHandler OnDeath;

        private static int currIndex = 0;

        private double mass;
        private double scale;
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
        private double radiusSquared;

        public void HandleOnDeath(object sender, OnDeathEventArgs e)
        {
            Console.WriteLine("He's dying");
        }

        public ModelObject(LoadResult result, double mass, double scale, Quaterniond orientation, Vector3d position)
        {
            groups = result.Groups;
            normals = result.Normals;
            vertices = result.Vertices;
            textures = result.Textures;
            materials = result.Materials;
            radiusSquared = result.RadiusSquared;
            this.mass = mass;
            this.scale = scale;
            this.orientation = orientation;
            this.position = position;
            velocity = Vector3d.Zero;
            destroyed = false;
            index = currIndex++;
            OnDeath += HandleOnDeath;
        }

        public ModelObject(LoadResult result, double mass, double scale)
            :this(result, mass, scale, Quaterniond.Identity,Vector3d.Zero) {}

        public ModelObject(LoadResult result)
            : this(result, 1, 1)
        { }

        public double Mass
        {
            get
            {
                return mass;
            }
        }

        public double Scale
        {
            get
            {
                return scale;
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

        public void Destroy(IModelObject destroyer)
        {
            if(!this.destroyed)
            {
                this.destroyed = true;
                OnDeath(this, new OnDeathEventArgs(this, destroyer));
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
                return orientation;
            }
            set
            {
                orientation = value;
            }
        }

        public Vector3d Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public IList<Group> Groups
        {
            get
            {
                return groups;
            }
        }

        public IList<Normal> Normals
        {
            get
            {
                return normals;
            }
        }

        public IList<Vertex> Vertices
        {
            get
            {
                return vertices;
            }
        }

        public IList<Texture> Textures
        {
            get
            {
                return textures;
            }
        }

        public IList<Material> Materials
        {
            get
            {
                return materials;
            }
        }

        public bool Destroyed
        {
            get
            {
                return destroyed;
            }
        }

        public Vector3d Velocity
        {
            get
            {
                return velocity;
            }

            set
            {
                velocity = value;
            }
        }

        public double RadiusSquared
        {
            get
            {
                return radiusSquared;
            }
        }

        public bool EqualsOtherObject(IModelObject obj)
        {
            if(obj is ModelObject)
            {
                return ((ModelObject)obj).index == index;
            }
            else
            {
                return obj.EqualsOtherObject(this);
            }
        }
    }
}
