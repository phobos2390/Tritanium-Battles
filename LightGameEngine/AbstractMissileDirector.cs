using ObjLoader.Loader.Loaders;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public abstract class AbstractMissileDirector: IMissileDirector
    {
        double thrust;
        double fuel;
        double blastRadius;
        double mass;

        private Model model;
        private IModelObject firedBy;
        private Vector3d offset;
        private LoadResult mesh;

        public AbstractMissileDirector(double thrust, double fuel, double blastRadius, double mass, Model model, IModelObject firedBy, Vector3d offset, LoadResult mesh)
        {
            this.thrust = thrust;
            this.fuel = fuel;
            this.blastRadius = blastRadius;
            this.mass = mass;
            this.model = model;
            this.firedBy = firedBy;
            this.offset = offset;
            this.mesh = mesh;
        }

        public Missile CreateMissile(MissileBuilder builder, ModelObjectBuilder baseBuilder, PropelledObjectBuilder propBuilder)
        {
            baseBuilder.SetMass(mass)
                       .SetOrientation(firedBy.Orientation)
                       .SetPosition(firedBy.Position)
                       .SetResult(mesh);
            propBuilder.SetFuel(fuel)
                       .SetThrust(thrust)
                       .SetBaseObject(baseBuilder.CreateModelObject());
            builder.SetBlastRadius(blastRadius)
                   .SetModel(model)
                   .SetFiredBy(firedBy)
                   .SetOffset(offset)
                   .SetPropelled(propBuilder.CreatePropelledObject());
            return builder.CreateMissile();
        }

        public Missile CreateMissile()
        {
            return CreateMissile(new MissileBuilder(), new ModelObjectBuilder(), new PropelledObjectBuilder());
        }
    }
}
