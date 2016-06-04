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
    public class ModelObjectFactory
    {
        private const string directory = "../../../ShipModels/";

        private static double CARPOMASS = 500;
        private static double DEIMOMASS = 1000000;
        
        const int UPPBOUNDS = 100;
        const int LOWBOUNDS = -100;

        MeshLoader loader;

        static Random rand = new Random();

        public static Vector3d randVec()
        {
            return new Vector3d(rand.Next(LOWBOUNDS, UPPBOUNDS), rand.Next(LOWBOUNDS, UPPBOUNDS), rand.Next(LOWBOUNDS, UPPBOUNDS));
        }

        public ModelObjectFactory()
        {
            loader = new MeshLoader(directory);
        }

        public ComputerControlledOpponent CreateUnarmedAI(ModelTypes shipType, Model model, Quaterniond orient, Vector3d initialPosition)
        {
            ComputerControlledOpponentBuilder opponent = new ComputerControlledOpponentBuilder();
            ComputerStateMachineBuilder stateMachineBuilder = new ComputerStateMachineBuilder();
            stateMachineBuilder.SetAIToPeaceFull()
                               .SetWaypoints(createRandomWayPoints());
            opponent.SetInitialState(stateMachineBuilder.CreateStates())
                    .SetShip(CreateCoreShip(shipType,model,orient,initialPosition));
            return opponent.CreateAI();
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
            ComputerControlledOpponentBuilder opponent = new ComputerControlledOpponentBuilder();
            ComputerStateMachineBuilder stateMachineBuilder = new ComputerStateMachineBuilder();
            stateMachineBuilder.SetAIToFighter()
                               .SetWaypoints(createRandomWayPoints());
            opponent.SetInitialState(stateMachineBuilder.CreateStates())
                    .SetShip(CreateCoreShip(shipType,model,orient,initialPosition));
            return opponent.CreateAI();
        }

        private ShipObject CreateCoreShip(ModelTypes shipType, Model model, Quaterniond orient, Vector3d initialPosition)
        {
            IShipDirector shipDirector = null;
            switch (shipType)
            {
                case ModelTypes.CarpoRefit:
                    shipDirector = new CarpoRefitDirector(model, initialPosition, orient, loader);
                    break;
                case ModelTypes.MSFV3:
                    shipDirector = new MSFV3Director(model, initialPosition, orient, loader);
                    break;
                case ModelTypes.MSFV5:
                    shipDirector = new MSFV5Director(model, initialPosition, orient, loader);
                    break;
                default:
                    break;
            }
            return shipDirector.CreateShip();
        }

        public IModelObject CreateAsteroid(Quaterniond orient, Vector3d position)
        {
            AsteroidDirector director = new AsteroidDirector(loader);
            return director.CreateAsteroid(position, orient);
        }

        public ControllableObject CreateControlledObject(ModelTypes shipType, Model model, Quaterniond orient, Vector3d initialPosition)
        {
            return new ControllableObject(CreateCoreShip(shipType, model, orient, initialPosition));
        }

        //public static IModelObject CreateModel(ModelTypes types)
        //{
        //    return new ModelObject(meshMap[types], massMap[types]);
        //}

        //public static IModelObject CreateModel(ModelTypes types, Quaterniond orientation, Vector3d position)
        //{
        //    return new ModelObject(meshMap[types], massMap[types], orientation, position);
        //}

        //public void CreateMissile(IModelObject firedBy, Vector3d offset, double blastRadius, double thrust, double fuel, double mass, ModelTypes type, Model model)
        //{
        //    model.AddModelObject(new Missile(blastRadius, model, offset + firedBy.Position, firedBy, new PropelledObject(thrust, fuel, CreateModel(type))));
        //}
    }
}
