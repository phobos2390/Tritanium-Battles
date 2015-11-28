using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ObjLoader.Loader.Loaders;
using OpenTK;
using ObjLoader.Loader.Data.VertexData;
using ObjLoader.Loader.Data.Elements;

namespace LightGameEngine.Model
{
    public class ModelObjectFactory:IMissileFactory
    {
        private static string MSFV3 = "MSFV3LowerPoly.obj";
        private static string MSFV5 = "MartianSpaceFighterVersion5.obj";
        private static string CARPO = "CarpoClass.obj";
        private static string CRPRF = "CarpoClassRefit.obj";
        private static string MISSL = "Missile.obj";
        private static string HMSSL = "HighMissile.obj";
        private static string ASTER = "Asteroid.obj";
        private static string EXPLO = "Explosion.obj";
        private static string DEIMO = "DeimosClass.obj";

        private static double MSFV3MASS = 430;
        private static double MSFV5MASS = 650;
        private static double CARPOMASS = 500;
        private static double ASTERMASS = 40000000000;
        private static double MISSLMASS = .125;
        private static double HMSSLMASS = .5;
        private static double DEIMOMASS = 1000000;
        private static double MISSLRAD = 5;
        private static double HMSSLRAD = 20;
        private static double SHIPTHRUST = 1000;
        private static double MISSTHRUST = 1000;
        private static double MISSFUEL = 10;
        private static double SHIPFUEL = 15000;
        private static int NUM_OF_MISSILES = 45;
        private static int NUM_OF_HIGH = 4;

        const int UPPBOUNDS = 100;
        const int LOWBOUNDS = -100;

        private static IDictionary<ModelTypes, LoadResult> meshMap = initMeshMap();
        private static IDictionary<ModelTypes, double> massMap = initMassMap();
        private static IDictionary<ModelTypes, IList<Tuple<ModelTypes, Vector3d>>> compMap = initCompMap();
        private static IDictionary<ModelTypes, double> boundRadMap = initBoundMap();
        private static IDictionary<ModelTypes, Alignment> alignmentMap = initAlignmentMap();

        private static IDictionary<ModelTypes,double> initBoundMap()
        {
            IDictionary<ModelTypes, double> dict = new Dictionary<ModelTypes, double>();
            dict[ModelTypes.MSFV3] = 8;
            dict[ModelTypes.MSFV5] = 13;
            dict[ModelTypes.Carpo] = 17;
            dict[ModelTypes.CarpoRefit] = 17;
            dict[ModelTypes.Deimos] = 117;
            return dict;
        }

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
            carpoComp.Add(Tuple.Create(ModelTypes.Missile, new Vector3d(5.25, .25, 0)));
            carpoComp.Add(Tuple.Create(ModelTypes.Missile, new Vector3d(-5.25, .25, 0)));
            carpoComp.Add(Tuple.Create(ModelTypes.HighMissile, new Vector3d(5.25, .25, 0)));
            carpoComp.Add(Tuple.Create(ModelTypes.HighMissile, new Vector3d(-5.25, .25, 0)));
            cMap[ModelTypes.MSFV3] = msfv3Comp;
            cMap[ModelTypes.MSFV5] = msfv5Comp;
            cMap[ModelTypes.CarpoRefit] = carpoComp;
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
            meshMap[ModelTypes.CarpoRefit] = loadMesh(CRPRF);
            meshMap[ModelTypes.Missile] = loadMesh(MISSL);
            meshMap[ModelTypes.HighMissile] = loadMesh(HMSSL);
            meshMap[ModelTypes.Asteroid] = loadMesh(ASTER);
            meshMap[ModelTypes.Explosion] = loadMesh(EXPLO);
            meshMap[ModelTypes.Deimos] = loadMesh(DEIMO);
            return meshMap;
        }

        private static IDictionary<ModelTypes, double> initMassMap()
        {
            IDictionary<ModelTypes, double> massMap = new Dictionary<ModelTypes, double>();
            massMap[ModelTypes.MSFV3] = MSFV3MASS;
            massMap[ModelTypes.MSFV5] = MSFV5MASS;
            massMap[ModelTypes.Carpo] = CARPOMASS;
            massMap[ModelTypes.CarpoRefit] = CARPOMASS;
            massMap[ModelTypes.Missile] = MISSLMASS;
            massMap[ModelTypes.HighMissile] = HMSSLMASS;
            massMap[ModelTypes.Asteroid] = ASTERMASS;
            massMap[ModelTypes.Explosion] = MISSLMASS;
            massMap[ModelTypes.Deimos] = DEIMOMASS;
            return massMap;
        }

        private static IDictionary<ModelTypes, double> initRadiusMap()
        {
            IDictionary<ModelTypes, double> massMap = new Dictionary<ModelTypes, double>();
            massMap[ModelTypes.Missile] = MISSLRAD;
            massMap[ModelTypes.HighMissile] = HMSSLRAD;
            return massMap;
        }

        private static IDictionary<ModelTypes,Alignment> initAlignmentMap()
        {
            IDictionary<ModelTypes, Alignment> massMap = new Dictionary<ModelTypes, Alignment>();
            massMap[ModelTypes.MSFV3] = Alignment.Martian;
            massMap[ModelTypes.MSFV5] = Alignment.Martian;
            massMap[ModelTypes.Carpo] = Alignment.Jovian;
            massMap[ModelTypes.CarpoRefit] = Alignment.Jovian;
            return massMap;
        }

        private static LoadResult loadMesh(string fileName)
        {
            string fullFileName = Path.GetFullPath(fileName);
            
            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create(new MaterialStreamProvider());

            var fileStream = new FileStream(fullFileName, FileMode.Open, FileAccess.Read);

            var result = objLoader.Load(fileStream);
            return result;
        }

        static Random rand = new Random();

        public static Vector3d randVec()
        {
            return new Vector3d(rand.Next(LOWBOUNDS, UPPBOUNDS), rand.Next(LOWBOUNDS, UPPBOUNDS), rand.Next(LOWBOUNDS, UPPBOUNDS));
        }

        public ModelObjectFactory()
        {
        }

        public static ModelObject LoadFromFile(string fileName)
        {
            return new ModelObject(loadMesh(fileName));
        }

        public MissileArray CreateComplement(ModelTypes missileType, Model model, Vector3d offset, int numOfMissiles, IModelObject firedBy)
        {
            return new MissileArray(numOfMissiles, massMap[missileType],blastRadiusMap[missileType],MISSTHRUST,MISSFUEL,firedBy, offset,missileType, model, this);
        }

        public ComputerControlledOpponent CreateUnarmedAI(ModelTypes shipType, Model model, Quaterniond orient, Vector3d initialPosition)
        {
            PatrollingState patrolling = new PatrollingState(createRandomWayPoints(), null, null);
            patrolling.FightingState = patrolling;
            var opponent = new ComputerControlledOpponent(CreateCoreShip(shipType, model, orient, initialPosition), patrolling);
            patrolling.Machine = opponent;
            return opponent;
        }

        private IList<Vector3d> createRandomWayPoints()
        {
            IList<Vector3d> wayPoints = new List<Vector3d>();
            for (int i = 0; i < 50; ++i)
            {
                wayPoints.Add(randVec());
            }
            return wayPoints;
        }

        public ComputerControlledOpponent CreateAIOpponent(ModelTypes shipType, Model model, Quaterniond orient, Vector3d initialPosition)
        {
            PatrollingState patrolling = new PatrollingState(createRandomWayPoints(), null, null);
            FightingState fighting = new FightingState(null, patrolling);
            patrolling.FightingState = fighting;
            var opponent = new ComputerControlledOpponent(CreateCoreShip(shipType, model, orient, initialPosition), patrolling);
            patrolling.Machine = opponent;
            fighting.Machine = opponent;
            return opponent;
        }

        private ShipObject CreateCoreShip(ModelTypes shipType, Model model, Quaterniond orient, Vector3d initialPosition)
        {
            IModelObject coreObject = CreateModel(shipType, orient, initialPosition);
            IList<MissileArray> complement = new List<MissileArray>();
            foreach (var val in compMap[shipType])
            {
                if (val.Item1 == ModelTypes.Missile)
                {
                    complement.Add(CreateComplement(val.Item1, model, val.Item2, NUM_OF_MISSILES, coreObject));
                }
                else
                {
                    complement.Add(CreateComplement(val.Item1, model, val.Item2, NUM_OF_HIGH, coreObject));
                }
            }
            return new ShipObject(SHIPTHRUST, SHIPFUEL, complement, coreObject, model, alignmentMap[shipType]);
        }

        public ControllableObject CreateControlledObject(ModelTypes shipType, Model model, Quaterniond orient, Vector3d initialPosition)
        {
            return new ControllableObject(CreateCoreShip(shipType, model, orient, initialPosition));
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
