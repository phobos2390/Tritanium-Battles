using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public interface IMissileDirector
    {
        Missile CreateMissile(MissileBuilder missileBuilder, ModelObjectBuilder modelBuilder, PropelledObjectBuilder propelledBuilder);
        Missile CreateMissile();
    }
}
