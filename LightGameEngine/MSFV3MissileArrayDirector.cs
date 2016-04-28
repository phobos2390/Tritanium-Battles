using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public class MSFV3MissileArrayDirector : AbstractComplementDirector
    {
        private const double XVAL = 2;
        private const double YVAL = 1.67432;
        private static int NUM_OF_MISSILES = 45;
        private static int NUM_OF_HIGH = 4;

        IList<Vector3d> offsets;

        public MSFV3MissileArrayDirector(IModelObject firedBy, double thrust, Model model, MeshLoader loader)
            : base(firedBy,thrust, model,loader)
        {
            offsets = new List<Vector3d>();
            offsets.Add(new Vector3d(XVAL, -YVAL, 0));
            offsets.Add(new Vector3d(-XVAL, -YVAL, 0));
        }

        public override IList<MissileArray> CreateComplement(MissileArrayBuilder builders)
        {
            return CreateComplement(builders, offsets, NUM_OF_MISSILES, NUM_OF_HIGH);
        }

        public override IList<MissileArray> CreateComplement()
        {
            return CreateComplement(new MissileArrayBuilder(), offsets, NUM_OF_MISSILES, NUM_OF_HIGH);
        }
    }
}
