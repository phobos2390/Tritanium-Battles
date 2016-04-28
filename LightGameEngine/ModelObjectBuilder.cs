using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjLoader.Loader.Loaders;
using OpenTK;

namespace LightGameEngine.Model
{
    public class ModelObjectBuilder
    {
        private LoadResult result;
        private double mass;
        private Quaterniond orientation;
        private Vector3d position;

        public ModelObjectBuilder SetResult(LoadResult result)
        {
            this.result = result;
            return this;
        }

        public ModelObjectBuilder SetMass(double mass)
        {
            this.mass = mass;
            return this;
        }

        public ModelObjectBuilder SetOrientation(Quaterniond orientation)
        {
            this.orientation = orientation;
            return this;
        }

        public ModelObjectBuilder SetPosition(Vector3d position)
        {
            this.position = position;
            return this;
        }

        public IModelObject CreateModelObject()
        {
            return new ModelObject(result, mass, orientation, position);
        }
    }
}
