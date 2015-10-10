using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ObjLoader.Loader.Loaders;

namespace LightGameEngine.Model
{
    public class ModelObjectFactory
    {
        private static string MSFV3 = "MSFV3LowerPoly.obj";
        private static string MSFV5 = "MartianSpaceFighterVersion5.obj";
        private static string CARPO = "CarpoClass.obj";
        private static string MISSL = "Missile.obj";
        private static string HMSSL = "HighMissile.obj";

        private static double MSFV3MASS = 4.3;
        private static double MSFV5MASS = 6.5;
        private static double CARPOMASS = 5;
        private static double MISSLMASS = 1;
        private static double HMSSLMASS = 1.5;

        private static IDictionary<ModelTypes, LoadResult> meshMap = initMeshMap();
        private static IDictionary<ModelTypes, double> massMap;

        private static IDictionary<ModelTypes,LoadResult> initMeshMap()
        {
            IDictionary<ModelTypes, LoadResult> meshMap = new Dictionary<ModelTypes, LoadResult>();
            meshMap[ModelTypes.MSFV3] = loadMesh(MSFV3);
            meshMap[ModelTypes.MSFV5] = loadMesh(MSFV5);
            meshMap[ModelTypes.Carpo] = loadMesh(CARPO);
            meshMap[ModelTypes.Missile] = loadMesh(MISSL);
            meshMap[ModelTypes.HighMissile] = loadMesh(HMSSL);
            return meshMap;
        }

        private static IDictionary<ModelTypes, double> initMassMap()
        {
            IDictionary<ModelTypes, double> massMap = new Dictionary<ModelTypes, double>();
            massMap[ModelTypes.MSFV3] = MSFV3MASS;
            massMap[ModelTypes.MSFV5] = MSFV5MASS;
            massMap[ModelTypes.Carpo] = CARPOMASS;
            massMap[ModelTypes.Missile] = MISSLMASS;
            massMap[ModelTypes.HighMissile] = HMSSLMASS;
            return massMap;
        }

        private static LoadResult loadMesh(string fileName)
        {
            string fullFileName = Path.GetFullPath(fileName);
            
            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create(new MaterialStreamProvider());

            var fileStream = new FileStream(fullFileName, FileMode.Open, FileAccess.Read);

            return objLoader.Load(fileStream);
        }

        public static ModelObject LoadFromFile(string fileName)
        {
            return new ModelObject(loadMesh(fileName));
        }

        public static IModelObject CreateModel(ModelTypes types)
        {
            return new ModelObject(meshMap[types], massMap[types]);
        }
    }
}
