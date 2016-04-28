using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public class CarpoRefitDirector : AbstractShipDirector
    {
        private static string CRPRF = "CarpoClassRefit.obj";

        private static double CARPOMASS = 500;
        private static double SHIPTHRUST = 1000;
        private static double SHIPFUEL = 15000;

        private Model model;
        private MeshLoader loader;

        public CarpoRefitDirector(Model model, Vector3d position, Quaterniond orientation, MeshLoader loader)
            :base(Alignment.Jovian,SHIPFUEL,SHIPTHRUST, CARPOMASS, model,position,orientation,loader.LoadMesh(CRPRF))
        {
            this.model = model;
            this.loader = loader;
        }

        public ShipObject CreateShip(ShipObjectBuilder shipBuilder, ModelObjectBuilder modelObjectBuilder)
        {
            IModelObject core = modelObjectBuilder.CreateModelObject();
            return CreateShip(shipBuilder, core, new CarpoMissileArrayDirector(core, SHIPTHRUST, model, loader));
        }

        public override ShipObject CreateShip()
        {
            ModelObjectBuilder modelObjectBuilder = new ModelObjectBuilder();
            IModelObject core = createCoreObject(modelObjectBuilder);
            return CreateShip(new ShipObjectBuilder(), core, new CarpoMissileArrayDirector(core, SHIPTHRUST, model, loader));
        }
    }
}
