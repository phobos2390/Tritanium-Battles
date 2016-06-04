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
        private double thrust;
        private double fuel;
        private double blastRadius;
        private double mass;
        private double scale;

        private double acceleration;
        private int duration;

        private Model model;
        private IModelObject firedBy;
        private IModelObject explosion;
        private Vector3d offset;
        private LoadResult mesh;
        private LoadResult explosionMesh;

        public AbstractMissileDirector(double thrust, double fuel, double blastRadius, double scale, double mass, Model model, IModelObject firedBy, Vector3d offset, LoadResult mesh, double explosionAcceleration, int explosionDuration, LoadResult explosionMesh)
        {
            this.thrust = thrust;
            this.fuel = fuel;
            this.blastRadius = blastRadius;
            this.mass = mass;
            this.model = model;
            this.firedBy = firedBy;
            this.offset = offset;
            this.mesh = mesh;
            this.scale = scale;

            acceleration = explosionAcceleration;
            duration = explosionDuration;
            this.explosionMesh = explosionMesh;
        }

        private Explosion createExplosion(ModelObjectBuilder baseBuilder, ExplosionBuilder explosionBuilder)
        {
            baseBuilder.SetMass(mass)
                       .SetOrientation(firedBy.Orientation)
                       .SetPosition(firedBy.Position)
                       .SetResult(explosionMesh);
            explosionBuilder.SetAcceleration(acceleration)
                            .SetDuration(duration)
                            .SetModelObject(baseBuilder.CreateModelObject());
            return explosionBuilder.CreateExplosion();
        }

        private Explosion createExplosion()
        {
            return createExplosion(new ModelObjectBuilder(), new ExplosionBuilder());
        }

        public Missile CreateMissile(MissileBuilder builder, ModelObjectBuilder baseBuilder, PropelledObjectBuilder propBuilder)
        {
            baseBuilder.SetMass(mass)
                       .SetScale(scale)
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
                   .SetPropelled(propBuilder.CreatePropelledObject())
                   .SetExplosion(createExplosion());
            return builder.CreateMissile();
        }

        public Missile CreateMissile()
        {
            return CreateMissile(new MissileBuilder(), new ModelObjectBuilder(), new PropelledObjectBuilder());
        }
    }
}
