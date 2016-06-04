using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public class PropelledObjectBuilder
    {
        double thrust;
        double fuel;
        IModelObject baseObject;

        public PropelledObjectBuilder SetThrust(double thrust)
        {
            this.thrust = thrust;
            return this;
        }

        public PropelledObjectBuilder SetFuel(double fuel)
        {
            this.fuel = fuel;
            return this;
        }

        public PropelledObjectBuilder SetBaseObject(IModelObject baseObj)
        {
            baseObject = baseObj;
            return this;
        }

        public PropelledObject CreatePropelledObject()
        {
            return new PropelledObject(thrust, fuel, baseObject);
        }
    }
}
