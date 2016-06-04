using ObjLoader.Loader.Loaders;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public class AsteroidDirector
    {
        const double ASTERMASS = 40000000000;
        const string ASTER = "Asteroid.obj";
        const double scale = 1;

        LoadResult result;

        public AsteroidDirector(MeshLoader loader)
        {
            result = loader.LoadMesh(ASTER);
        }

        public IModelObject CreateAsteroid(ModelObjectBuilder builder, Vector3d position, Quaterniond orientation)
        {
            builder.SetMass(ASTERMASS)
                   .SetScale(scale)
                   .SetPosition(position)
                   .SetOrientation(orientation)
                   .SetResult(result);
            return builder.CreateModelObject();
        }

        public IModelObject CreateAsteroid(Vector3d position, Quaterniond orientation)
        {
            return CreateAsteroid(new ModelObjectBuilder(), position, orientation);
        }
    }
}
