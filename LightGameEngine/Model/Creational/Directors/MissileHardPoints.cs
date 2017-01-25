using OpenTK;

namespace LightGameEngine.Model
{
    public class MissileHardPoint
    {
        private Vector3d location;

        public MissileHardPoint(Vector3d loc)
        {
            location = loc;
        }

        public Vector3d Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
            }
        }
    }
}