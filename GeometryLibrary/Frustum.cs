using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace GeometryLibrary
{
    public class Frustum
    {
        private Matrix4d viewMatrix;
        private Angle fovy;
        private double aspect;
        private double near;
        private double far;

        public Frustum(Angle fovy, double aspect, double near, double far)
        {
            this.viewMatrix = Matrix4d.CreatePerspectiveFieldOfView(fovy.Radians, aspect, near, far);
            this.fovy = fovy;
            this.aspect = aspect;
            this.near = near;
            this.far = far;
        }

        public Matrix4d Matrix
        {
            get
            {
                return this.viewMatrix;
            }
        }
    }
}
