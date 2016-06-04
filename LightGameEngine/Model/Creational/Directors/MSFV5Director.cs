using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    class MSFV5Director : AbstractShipDirector
    {
        private static string MSFV5 = "MartianSpaceFighterVersion5.obj";

        private static double MSFV5MASS = 650;
        private static double SHIPTHRUST = 1000;
        private static double SHIPFUEL = 15000;
        private static double SCALE = 1;

        private Model model;
        private MeshLoader loader;

        public MSFV5Director(Model model, Vector3d position, Quaterniond orientation, MeshLoader loader)
            :base(Alignment.Martian,SHIPFUEL,SHIPTHRUST,MSFV5MASS, SCALE,model,position,orientation,loader.LoadMesh(MSFV5))
        {
            this.model = model;
            this.loader = loader;
        }

        public ShipObject CreateShip(ShipObjectBuilder shipBuilder, ModelObjectBuilder modelObjectBuilder)
        {
            IModelObject core = modelObjectBuilder.CreateModelObject();
            return CreateShip(shipBuilder, core, new MSFV5MissileArrayDirector(core, SHIPTHRUST, model, loader));
        }

        public override ShipObject CreateShip()
        {
            ModelObjectBuilder modelObjectBuilder = new ModelObjectBuilder();
            IModelObject core = createCoreObject(modelObjectBuilder);
            return CreateShip(new ShipObjectBuilder(), core, new MSFV5MissileArrayDirector(core, SHIPTHRUST, model, loader));
        }
    }
}
