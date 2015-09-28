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

namespace LightGameEngine
{
    public class ModelObject
    {
        private Angle xRotation;
        private Angle yRotation;
        private Angle zRotation;
        private Vector3 position;
        private Vector3 velocity;
        private double acceleration;
        private int textureIndex;
        private IList<Group> groups;
        private IList<Normal> normals;
        private IList<Vertex> vertices;
        private IList<Texture> textures;
        private IList<Material> materials;

        public ModelObject(LoadResult result)
        {
            this.groups = result.Groups;
            this.normals = result.Normals;
            this.vertices = result.Vertices;
            this.textures = result.Textures;
            this.materials = result.Materials;
        }

        public void OnUpdate()
        {
            Vector3 accelVector = new Vector3(
                (float)(yRotation.Cosine() * xRotation.Cosine() * this.acceleration),
                (float)(xRotation.Sine() * this.acceleration),
                (float)(yRotation.Sine() * xRotation.Cosine() * this.acceleration));
            velocity += accelVector;
            position += velocity;
        }

        public double Acceleration
        {
            get
            {
                return this.acceleration;
            }
            set
            {
                this.acceleration = value;
            }
        }

        public Angle XRotation
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

        public Angle YRotation
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

        public Angle ZRotation
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

        public Vector3 Position
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
