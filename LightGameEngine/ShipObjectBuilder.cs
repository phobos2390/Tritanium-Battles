using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public class ShipObjectBuilder
    {
        private IModelObject coreObject;
        private IList<MissileArray> complement;
        private double thrust;
        private double fuel;
        private Model model;
        private Alignment alignment;

        public ShipObjectBuilder()
        {
            complement = new List<MissileArray>();
        }

        public ShipObjectBuilder SetCoreObject(IModelObject coreObject)
        {
            this.coreObject = coreObject;
            return this;
        }

        public ShipObjectBuilder SetMissileComplement(IList<MissileArray> complement)
        {
            this.complement = complement;
            return this;
        }

        public ShipObjectBuilder AddMissileArray(MissileArray missileArray)
        {
            complement.Add(missileArray);
            return this;
        }

        public ShipObjectBuilder SetThrust(double thrust)
        {
            this.thrust = thrust;
            return this;
        }

        public ShipObjectBuilder SetFuel(double fuel)
        {
            this.fuel = fuel;
            return this;
        }
        
        public ShipObjectBuilder SetModel(Model model)
        {
            this.model = model;
            return this;
        }

        public ShipObjectBuilder SetAlignment(Alignment alignment)
        {
            this.alignment = alignment;
            return this;
        }

        public ShipObject CreateShip()
        {

            return new ShipObject(thrust, fuel, complement, coreObject, model, alignment);
        }
    }
}
