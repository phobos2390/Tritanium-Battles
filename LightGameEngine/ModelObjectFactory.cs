using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ObjLoader.Loader.Loaders;
using OpenTK;

namespace LightGameEngine.Model
{
    public class ModelObjectFactory:IMissileFactory
    {
        private static string MSFV3 = "MSFV3LowerPoly.obj";
        private static string MSFV5 = "MartianSpaceFighterVersion5.obj";
        private static string CARPO = "CarpoClass.obj";
        private static string MISSL = "Missile.obj";
        private static string HMSSL = "HighMissile.obj";
        private static string ASTER = "Asteroid.obj";
        private static string EXPLO = "Explosion.obj";

        private static double MSFV3MASS = 4.3;
        private static double MSFV5MASS = 6.5;
        private static double CARPOMASS = 5;
        private static double ASTERMASS = 5;
        private static double MISSLMASS = 1;
        private static double HMSSLMASS = 1.5;
        private static double MISSLRAD = 30;
        private static double HMSSLRAD = 150;
        private static double SHIPTHRUST = 25;
        private static double MISSTHRUST = 10;
        private static double MISSFUEL = 10;
        private static double SHIPFUEL = 1500;
        private static int NUM_OF_MISSILES = 30;

        private static IDictionary<ModelTypes, LoadResult> meshMap = initMeshMap();
        private static IDictionary<ModelTypes, double> massMap = initMassMap();

        private static IDictionary<ModelTypes, double> blastRadiusMap = initRadiusMap();
        
        private static IDictionary<ModelTypes,LoadResult> initMeshMap()
        {
            IDictionary<ModelTypes, LoadResult> meshMap = new Dictionary<ModelTypes, LoadResult>();
            meshMap[ModelTypes.MSFV3] = loadMesh(MSFV3);
            meshMap[ModelTypes.MSFV5] = loadMesh(MSFV5);
            meshMap[ModelTypes.Carpo] = loadMesh(CARPO);
            meshMap[ModelTypes.Missile] = loadMesh(MISSL);
            meshMap[ModelTypes.HighMissile] = loadMesh(HMSSL);
            meshMap[ModelTypes.Asteroid] = loadMesh(ASTER);
            meshMap[ModelTypes.Explosion] = loadMesh(EXPLO);
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
            massMap[ModelTypes.Asteroid] = ASTERMASS;
            massMap[ModelTypes.Explosion] = MISSLMASS;
            return massMap;
        }

        private static IDictionary<ModelTypes, double> initRadiusMap()
        {
            IDictionary<ModelTypes, double> massMap = new Dictionary<ModelTypes, double>();
            massMap[ModelTypes.Missile] = MISSLRAD;
            massMap[ModelTypes.HighMissile] = HMSSLRAD;
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

        public MissileArray CreateComplement(ModelTypes missileType, Model model, IModelObject firedBy)
        {
            return new MissileArray(NUM_OF_MISSILES,massMap[missileType],blastRadiusMap[missileType],MISSTHRUST,MISSFUEL,firedBy,missileType, model, this);
        }

        public ControllableObject CreateControlledObject(ModelTypes shipType, Model model, ModelTypes missileType)
        {
            IModelObject coreObject = CreateModel(shipType);
            return new ControllableObject(new ShipObject(SHIPTHRUST, SHIPFUEL, CreateComplement(missileType, model, coreObject), coreObject));
        }

        public static IModelObject CreateModel(ModelTypes types)
        {
            return new ModelObject(meshMap[types], massMap[types]);
        }

        public static IModelObject CreateModel(ModelTypes types, double pitchD, double yawD, double rollD, Vector3d position)
        {
            return CreateModel(types, Angle.CreateDegree(pitchD), Angle.CreateDegree(yawD), Angle.CreateDegree(rollD), position);
        }

        public static IModelObject CreateModel(ModelTypes types, Angle pitch, Angle yaw, Angle roll, Vector3d position)
        {
            return new ModelObject(meshMap[types], massMap[types], yaw, pitch, roll, position);
        }

        public void CreateMissile(IModelObject firedBy, double blastRadius, double thrust, double fuel, double mass, ModelTypes type, Model model)
        {
            model.AddModelObject(new Missile(blastRadius, model, firedBy, new PropelledObject(thrust, fuel, CreateModel(type))));
        }
    }
}
