using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public class MissileArrayBuilder
    {
        int numOfMissiles;
        double missileThrust;
        IModelObject firedBy;
        Model model;
        IMissileDirector director;

        public MissileArrayBuilder SetMissileDirector(IMissileDirector director)
        {
            this.director = director;
            return this;
        }

        public MissileArrayBuilder SetNumberOfMissiles(int missiles)
        {
            numOfMissiles = missiles;
            return this;
        }
        
        public MissileArrayBuilder SetMissileThrust(double thrust)
        {
            missileThrust = thrust;
            return this;
        }

        public MissileArrayBuilder SetFiredBy(IModelObject firedBy)
        {
            this.firedBy = firedBy;
            return this;
        }

        public MissileArrayBuilder SetModel(Model model)
        {
            this.model = model;
            return this;
        }

        public MissileArray CreateMissileArray()
        {
            return new MissileArray(numOfMissiles, director, model, firedBy, missileThrust);            
        }
    }
}
