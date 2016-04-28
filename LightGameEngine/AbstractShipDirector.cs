using ObjLoader.Loader.Loaders;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public abstract class AbstractShipDirector:IShipDirector
    {
        private Alignment alignment;
        private double fuel;
        private double thrust;
        private double mass;
        private Model model;
        private Vector3d position;
        private Quaterniond orientation;
        private LoadResult result;

        public AbstractShipDirector(Alignment alignment, double fuel, double thrust, double mass, Model model, Vector3d position, Quaterniond orientation, LoadResult result)
        {
            this.alignment = alignment;
            this.fuel = fuel;
            this.thrust = thrust;
            this.model = model;
            this.mass = mass;
            this.result = result;
            this.position = position;
            this.orientation = orientation;
        }

        protected IModelObject createCoreObject(ModelObjectBuilder builder)
        {
            return builder.SetResult(result)
                          .SetPosition(position)
                          .SetOrientation(orientation)
                          .SetMass(mass)
                          .CreateModelObject();
        }

        protected ShipObjectBuilder CreateShipBuilder(ShipObjectBuilder builder)
        {
            return builder.SetAlignment(alignment)
                          .SetFuel(fuel)
                          .SetModel(model)
                          .SetThrust(thrust);
        }

        protected ShipObject CreateShip(ShipObjectBuilder shipBuilder, IModelObject core, IComplementDirector director)
        {
            shipBuilder = CreateShipBuilder(shipBuilder);
            shipBuilder.SetCoreObject(core)
                       .SetMissileComplement(director.CreateComplement());
            return shipBuilder.CreateShip();
        }

        public abstract ShipObject CreateShip();
    }
}
