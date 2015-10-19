using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public class Model
    {
        private List<IModelObject> objects;

        public Model()
        {
            this.objects = new List<IModelObject>();
        }

        public void AddModelObject(IModelObject modelObject)
        {
            this.objects.Add(modelObject);
        }

        public void DestroyObject(IModelObject modelObject)
        {
            Console.WriteLine("Trying to destroy object");
            //IModelObject obj = ModelObjectFactory.CreateModel(ModelTypes.Explosion, 0, 0, 0, modelObject.Position);
            //this.objects.Add(modelObject);
        }

        public void OnUpdate(FrameEventArgs e)
        {
            try
            {
                foreach (IModelObject obj in this.objects)
                {
                    obj.OnUpdate(e);
                }
            }
            catch(Exception exp)
            {
                Console.WriteLine("Exception on update: {0}", exp);
            }
        }

        public List<IModelObject> Objects
        {
            get
            {
                return this.objects;
            }
        }
    }
}
