using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public class MissileBuilder
    {
        double blastRadius;
        Model model;
        Vector3d offset;
        IModelObject firedBy;
        IModelObject explosion;
        PropelledObject propelled;

        public MissileBuilder SetBlastRadius(double blastRadius)
        {
            this.blastRadius = blastRadius;
            return this;
        }

        public MissileBuilder SetModel(Model model)
        {
            this.model = model;
            return this;
        }

        public MissileBuilder SetOffset(Vector3d offset)
        {
            this.offset = offset;
            return this;
        }

        public MissileBuilder SetFiredBy(IModelObject firedBy)
        {
            this.firedBy = firedBy;
            return this;
        }

        public MissileBuilder SetExplosion(IModelObject explosion)
        {
            this.explosion = explosion;
            return this;
        }

        public MissileBuilder SetPropelled(PropelledObject propelledObject)
        {
            propelled = propelledObject;
            return this;
        }

        public Missile CreateMissile()
        {
            return new Missile(blastRadius, model, firedBy.Position + Vector3d.Transform(offset, firedBy.Orientation), firedBy, propelled, explosion);
        }
    }
}
