using LightGameEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine
{
    public class OnSightEventArgs : EventArgs
    {
        private IModelObject obj;

        public OnSightEventArgs(IModelObject obj)
            : base()
        {
            this.obj = obj;
        }

        public IModelObject SeenObject
        {
            get
            {
                return obj;
            }
        }
    }
}
