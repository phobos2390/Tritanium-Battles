namespace LightGameEngine.Model
{
    public class MissileSpecification
    {
        private int hardpoint;
        private string missileType;
        private int count;

        public MissileSpecification(int hardPoint, string type, int count)
        {
            hardpoint = hardPoint;
            missileType = type;
            this.count = count;
        }

        public int Count
        {
            get
            {
                return count;
            }

            set
            {
                count = value;
            }
        }

        public string MissileType
        {
            get
            {
                return missileType;
            }

            set
            {
                missileType = value;
            }
        }

        public int Hardpoint
        {
            get
            {
                return hardpoint;
            }

            set
            {
                hardpoint = value;
            }
        }
    }
}