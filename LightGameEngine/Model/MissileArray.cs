using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public class MissileArray
    {
        private int numberOfMissiles;
        private double thrust;
        private Model model;
        private IModelObject firedBy;

        private IMissileDirector director;

        public MissileArray(int missiles, IMissileDirector director, Model model, IModelObject firedBy, double thrust)
        {
            numberOfMissiles = missiles;
            this.director = director;
            this.model = model;
            this.firedBy = firedBy;
            this.thrust = thrust;
        }

        public void Fire()
        {
            if(numberOfMissiles-- > 0)
            {
                Missile missile = director.CreateMissile();
                model.AddModelObject(missile);
                missile.Velocity = new Vector3d(firedBy.Velocity.X, firedBy.Velocity.Y, firedBy.Velocity.Z);
                Vector3d accelVector = Vector3d.Transform(Vector3d.UnitZ,firedBy.Orientation);
                accelVector.NormalizeFast();
                firedBy.AddForce(Vector3d.Multiply(accelVector, thrust));
            }
            else
            {
                numberOfMissiles = 0;
            }
        }

        public int Count
        {
            get
            {
                return numberOfMissiles;
            }
        }
    }
}
