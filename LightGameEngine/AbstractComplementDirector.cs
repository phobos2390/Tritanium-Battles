using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public abstract class AbstractComplementDirector:IComplementDirector
    {
        IModelObject firedBy;
        double thrust;
        Model model;
        MeshLoader loader;

        public AbstractComplementDirector(IModelObject firedBy, double thrust, Model model, MeshLoader loader)
        {
            this.firedBy = firedBy;
            this.thrust = thrust;
            this.model = model;
            this.loader = loader;
        }

        protected Model containedModel
        {
            get
            {
                return model;
            }
        }

        protected IModelObject containedFiredBy
        {
            get
            {
                return firedBy;
            }
        }

        protected MissileArrayBuilder CreateMissileArrayBuilder(MissileArrayBuilder builder)
        {
            return builder.SetFiredBy(firedBy)
                          .SetMissileThrust(thrust)
                          .SetModel(model);
        }
        
        protected MissileArrayBuilder CreateMissileArrayBuilder()
        {
            return CreateMissileArrayBuilder(new MissileArrayBuilder());
        }

        private IList<Tuple<IMissileDirector, int>> createDirectors(IList<Vector3d> offsets, int missiles, int highMissiles)
        {
            IList<Tuple<IMissileDirector, int>> directors = new List<Tuple<IMissileDirector, int>>();
            directors.Add(Tuple.Create<IMissileDirector, int>(new StandardMissileDirector(containedModel, containedFiredBy, offsets[0], loader), missiles));
            directors.Add(Tuple.Create<IMissileDirector, int>(new StandardMissileDirector(containedModel, containedFiredBy, offsets[1], loader), missiles));
            directors.Add(Tuple.Create<IMissileDirector, int>(new HighMissileDirector(containedModel, containedFiredBy, offsets[0], loader), highMissiles));
            directors.Add(Tuple.Create<IMissileDirector, int>(new HighMissileDirector(containedModel, containedFiredBy, offsets[1], loader), highMissiles));
            return directors;
        }

        protected IList<MissileArray> CreateComplement(MissileArrayBuilder builder, IList<Vector3d> offsets, int missiles, int highMissiles)
        {
            builder = CreateMissileArrayBuilder(builder);
            IList<MissileArray> complement = new List<MissileArray>();
            IList<Tuple<IMissileDirector, int>> directors = createDirectors(offsets, missiles, highMissiles);
            for (int i = 0; i < directors.Count; ++i)
            {
                builder.SetMissileDirector(directors[i].Item1)
                       .SetNumberOfMissiles(directors[i].Item2);
                complement.Add(builder.CreateMissileArray());
            }
            return complement;
        }

        public abstract IList<MissileArray> CreateComplement(MissileArrayBuilder builders);
        public abstract IList<MissileArray> CreateComplement();
    }
}
