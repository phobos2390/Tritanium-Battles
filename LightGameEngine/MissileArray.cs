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
        private double massOfMissiles;
        private double blastRadius;
        private double thrust;
        private double fuel;
        private Vector3d positionOffset;
        private IMissileFactory factory;
        private ModelTypes missileTypes;
        private Model model;
        private IModelObject firedBy;

        public MissileArray(int missiles, double massOfMissiles, double blastRadius, double thrust, double fuel, IModelObject firedBy, Vector3d positionOffset, ModelTypes missileType, Model model, IMissileFactory factory)
        {
            this.positionOffset = positionOffset;
            this.numberOfMissiles = missiles;
            this.massOfMissiles = massOfMissiles;
            this.blastRadius = blastRadius;
            this.thrust = thrust;
            this.fuel = fuel;
            this.factory = factory;
            this.missileTypes = missileType;
            this.model = model;
            this.firedBy = firedBy;
        }

        public void Fire()
        {
            if(this.numberOfMissiles-- > 0)
            {
                this.factory.CreateMissile(this.firedBy, Vector3d.Transform(this.positionOffset,this.firedBy.Orientation), blastRadius, thrust, fuel, massOfMissiles, missileTypes, model);
                Vector3d accelVector = Vector3d.Transform(Vector3d.UnitZ,this.firedBy.Orientation);
                accelVector.NormalizeFast();
                this.firedBy.AddForce(Vector3d.Multiply(accelVector, thrust));
            }
        }

        public int Count
        {
            get
            {
                return this.numberOfMissiles;
            }
        }
    }
}
