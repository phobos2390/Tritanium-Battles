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
        private static double MISSLMASS = .0625;
        private static double HMSSLMASS = .125;
        private static double MISSLRAD = 5;
        private static double HMSSLRAD = 20;
        private static double SHIPTHRUST = 25;
        private static double MISSTHRUST = 10;
        private static double MISSFUEL = 10;
        private static double SHIPFUEL = 1500;
        private static int NUM_OF_MISSILES = 15;
        private static int NUM_OF_HIGH = 4;

        private static IDictionary<ModelTypes, LoadResult> meshMap = initMeshMap();
        private static IDictionary<ModelTypes, double> massMap = initMassMap();
        private static IDictionary<ModelTypes, IList<Tuple<ModelTypes, Vector3d>>> compMap = initCompMap();

        private static IDictionary<ModelTypes, IList<Tuple<ModelTypes, Vector3d>>> initCompMap()
        {
            IDictionary<ModelTypes, IList<Tuple<ModelTypes, Vector3d>>> cMap = new Dictionary<ModelTypes, IList<Tuple<ModelTypes, Vector3d>>>();
            IList<Tuple<ModelTypes, Vector3d>> msfv3Comp = new List<Tuple<ModelTypes, Vector3d>>();
            msfv3Comp.Add(Tuple.Create(ModelTypes.Missile, new Vector3d(2, -1.67432, 0)));
            msfv3Comp.Add(Tuple.Create(ModelTypes.Missile, new Vector3d(-2, -1.67432, 0)));
            msfv3Comp.Add(Tuple.Create(ModelTypes.HighMissile, new Vector3d(2, -1.67432, 0)));
            msfv3Comp.Add(Tuple.Create(ModelTypes.HighMissile, new Vector3d(-2, -1.67432, 0)));
            IList<Tuple<ModelTypes, Vector3d>> msfv5Comp = new List<Tuple<ModelTypes, Vector3d>>();
            msfv5Comp.Add(Tuple.Create(ModelTypes.Missile, new Vector3d(2.5, -2.25, -5)));
            msfv5Comp.Add(Tuple.Create(ModelTypes.Missile, new Vector3d(-2.5, -2.25, -5)));
            msfv5Comp.Add(Tuple.Create(ModelTypes.HighMissile, new Vector3d(2.5, -2.25, -5)));
            msfv5Comp.Add(Tuple.Create(ModelTypes.HighMissile, new Vector3d(-2.5, -2.25, -5)));
            IList<Tuple<ModelTypes, Vector3d>> carpoComp = new List<Tuple<ModelTypes, Vector3d>>();
            cMap[ModelTypes.MSFV3] = msfv3Comp;
            cMap[ModelTypes.MSFV5] = msfv5Comp;
            cMap[ModelTypes.Carpo] = carpoComp;
            return cMap;
        }

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

        public MissileArray CreateComplement(ModelTypes missileType, Model model, Vector3d offset, IModelObject firedBy)
        {
            return new MissileArray(NUM_OF_MISSILES,massMap[missileType],blastRadiusMap[missileType],MISSTHRUST,MISSFUEL,firedBy, offset,missileType, model, this);
        }

        public ControllableObject CreateControlledObject(ModelTypes shipType, Model model)
        {
            IModelObject coreObject = CreateModel(shipType);
            IList<MissileArray> complement = new List<MissileArray>();
            foreach (var val in compMap[shipType])
            {
                complement.Add(CreateComplement(val.Item1, model, val.Item2, coreObject));
            }
            return new ControllableObject(new ShipObject(SHIPTHRUST, SHIPFUEL, complement, coreObject));
        }

        public static IModelObject CreateModel(ModelTypes types)
        {
            return new ModelObject(meshMap[types], massMap[types]);
        }

        public static IModelObject CreateModel(ModelTypes types, Quaterniond orientation, Vector3d position)
        {
            return new ModelObject(meshMap[types], massMap[types], orientation, position);
        }

        public void CreateMissile(IModelObject firedBy, Vector3d offset, double blastRadius, double thrust, double fuel, double mass, ModelTypes type, Model model)
        {
            model.AddModelObject(new Missile(blastRadius, model, offset + firedBy.Position, firedBy, new PropelledObject(thrust, fuel, CreateModel(type))));
        }
    }
}
