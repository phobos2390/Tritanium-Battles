using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public class Model
    {
        private List<ModelObject> objects;

        public Model()
        {

        }

        public void addModelObject(ModelObject modelObject)
        {
            this.objects.Add(modelObject);
        }

        public void OnUpdate()
        {
            foreach(ModelObject obj in this.objects)
            {
                obj.OnUpdate();
            }
        }

        public List<ModelObject> Objects
        {
            get
            {
                return this.objects;
            }
        }
    }
}
