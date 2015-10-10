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
            this.objects.Remove(modelObject);
        }

        public void OnUpdate(FrameEventArgs e)
        {
            foreach(IModelObject obj in this.objects)
            {
                obj.OnUpdate(e);
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
