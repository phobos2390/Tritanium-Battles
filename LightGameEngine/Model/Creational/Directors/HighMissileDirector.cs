using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    class HighMissileDirector:AbstractMissileDirector
    {
        private static double MISSTHRUST = 1000;
        private static double MISSFUEL = 10;
        private static double HMSSLMASS = .5;
        private static double HMSSLRAD = 20;
        private static double SCALE = 1;

        private static double EXPLOACCEL = 5;
        private static int EXPLODUR = 420;

        private static string HMSSL = "HighMissile.obj";
        private static string EXPLO = "Explosion.obj";

        public HighMissileDirector(Model model, IModelObject firedBy, Vector3d offset, MeshLoader loader)
            :base(MISSTHRUST,MISSFUEL,HMSSLRAD,HMSSLMASS,SCALE,model,firedBy,offset,loader.LoadMesh(HMSSL), EXPLOACCEL, EXPLODUR, loader.LoadMesh(EXPLO)) { }
    }
}
