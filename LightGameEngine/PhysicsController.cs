using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Controller
{
    public class PhysicsController
    {
        static double GRAVITATIONAL_CONST = 6.67408E-11;
        private Model.Model model;

        public PhysicsController(Model.Model model)
        {
            this.model = model;
        }

        public void OnUpdateFrame(FrameEventArgs e)
        {
            model.OnUpdate(e);

            foreach(Model.IModelObject modObj in model.Objects)
            {
                foreach (Model.IModelObject modObj2 in model.Objects)
                {
                    if(modObj != modObj2)
                    {
                        Vector3d displacement = modObj2.Position - modObj.Position;
                        double r = displacement.LengthSquared;
                        double m1m2 = modObj.Mass * modObj2.Mass;
                        displacement.Normalize();
                        displacement = Vector3d.Multiply(displacement, GRAVITATIONAL_CONST * m1m2 / r);
                    }
                }
            }
        }
    }
}
