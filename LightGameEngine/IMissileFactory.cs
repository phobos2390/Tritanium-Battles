using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public interface IMissileFactory
    {
        void CreateMissile(IModelObject firedBy, Vector3d positionalOffset, double blastRadius, double thrust, double fuel, double mass, ModelTypes type, Model model);
        void AddMissile();
    }
}
