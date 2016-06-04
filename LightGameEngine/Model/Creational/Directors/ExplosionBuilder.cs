using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public class ExplosionBuilder
    {
        private double acceleration;
        private int duration;
        private IModelObject obj;

        public ExplosionBuilder SetAcceleration(double acceleration)
        {
            this.acceleration = acceleration;
            return this;
        }

        public ExplosionBuilder SetDuration(int ticks)
        {
            duration = ticks;
            return this;
        }

        public ExplosionBuilder SetModelObject(IModelObject obj)
        {
            this.obj = obj;
            return this;
        }

        public Explosion CreateExplosion()
        {
            return new Explosion(obj, acceleration, duration);
        }
    }
}
