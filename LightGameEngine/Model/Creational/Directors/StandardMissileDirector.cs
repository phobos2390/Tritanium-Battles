using ObjLoader.Loader.Loaders;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjLoader;
using System.IO;

namespace LightGameEngine.Model
{
    public class StandardMissileDirector:AbstractMissileDirector
    {
        private static double MISSTHRUST = 1000;
        private static double MISSFUEL = .25;
        private static double MISSLRAD = 5;
        private static double MISSLMASS = .125;
        private static double SCALE = 1;
        private static double EXPLOACCEL = 1;
        private static int EXPLODUR = 250;

        private static string MISSL = "Missile.obj";

        private static string EXPLO = "Explosion.obj";

        public StandardMissileDirector(Model model, IModelObject firedBy, Vector3d offset, MeshLoader loader)
            :base(MISSTHRUST,MISSFUEL,MISSLRAD,MISSLMASS,SCALE,model,firedBy,offset,loader.LoadMesh(MISSL),EXPLOACCEL,EXPLODUR,loader.LoadMesh(EXPLO)) {}
    }
}
