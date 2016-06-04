using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public class MSFV3Director:AbstractShipDirector
    {
        private static string MSFV3 = "MSFV3LowerPoly.obj";

        private static double MSFV3MASS = 430;
        private static double SHIPTHRUST = 1000;
        private static double SHIPFUEL = 15000;
        private static double SCALE = 1;

        private Model model;
        private MeshLoader loader;

        public MSFV3Director(Model model, Vector3d position, Quaterniond orientation, MeshLoader loader)
            :base(Alignment.Martian,SHIPFUEL,SHIPTHRUST,MSFV3MASS, SCALE,model,position,orientation,loader.LoadMesh(MSFV3))
        {
            this.model = model;
            this.loader = loader;
        }

        public ShipObject CreateShip(ShipObjectBuilder shipBuilder, ModelObjectBuilder modelObjectBuilder)
        {
            IModelObject core = modelObjectBuilder.CreateModelObject();
            return CreateShip(shipBuilder, core, new MSFV3MissileArrayDirector(core, SHIPTHRUST, model, loader));
        }

        public override ShipObject CreateShip()
        {
            ModelObjectBuilder modelObjectBuilder = new ModelObjectBuilder();
            IModelObject core = createCoreObject(modelObjectBuilder);
            return CreateShip(new ShipObjectBuilder(), core, new MSFV3MissileArrayDirector(core, SHIPTHRUST, model, loader));
        }
    }
}
