using LightGameEngine.Collision;
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
        private IList<IModelObject> objects;
        private IList<IModelObject> addedObjects;
        private bool modifiable;

        public Model()
        {
            this.objects = new List<IModelObject>();
            addedObjects = new List<IModelObject>();
            modifiable = true;
        }

        public void AddModelObject(IModelObject modelObject)
        {
            if (!modifiable)
            {
                addedObjects.Add(modelObject);
            }
            else
            {
                objects.Add(modelObject);
            }
        }

        public void DestroyObject(IModelObject modelObject)
        {
            Console.WriteLine("Trying to destroy object");
            //IModelObject obj = ModelObjectFactory.CreateModel(ModelTypes.Explosion, 0, 0, 0, modelObject.Position);
            //this.objects.Add(modelObject);
        }

        public void OnUpdate(FrameEventArgs e)
        {
            modifiable = false;
            foreach (IModelObject obj in this.objects)
            {
                obj.OnUpdate(e);
            }
            modifiable = true;
            for(int i = addedObjects.Count - 1; i >= 0; --i)
            {
                objects.Add(addedObjects[i]);
            }
            for (int i = this.objects.Count - 1; i >= 0; --i)
            {
                if (this.objects[i].Destroyed)
                {
                    this.objects.RemoveAt(i);
                }
            }
        }

        public IList<IModelObject> Objects
        {
            get
            {
                return this.objects;
            }
        }

        public Tuple<IModelObject, Vector3d> IntersectScene(Vector3d initial, Vector3d ray, IModelObject notMe)
        {
            IModelObject returnShape = null;
            double lastDistSquared = 0;
            Vector3d intersectionPoint = Vector3d.Zero;
            Vector3d normal = Vector3d.Zero;
            foreach (IModelObject obj in this.objects)
            {
                if (!notMe.EqualsOtherObject(obj))
                {
                    Sphere sphere = new Sphere(obj.Position, obj.RadiusSquared);
                    var ret = sphere.IntersectionVals(initial, ray);
                    if (ret.Item1)
                    {
                        Vector3d intersection = sphere.Intersection(initial, ret.Item2, ray, obj.RadiusSquared, ret.Item4, ret.Item5);
                        double distSquared = (intersection - initial).LengthSquared;
                        if (returnShape == null || lastDistSquared > distSquared)
                        {
                            returnShape = obj;
                            lastDistSquared = distSquared;
                            intersectionPoint = intersection;
                        }
                    }
                }
            }
            return Tuple.Create(returnShape, intersectionPoint);
        }
    }
}
