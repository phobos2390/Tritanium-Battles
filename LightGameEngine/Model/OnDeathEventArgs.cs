using LightGameEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine
{
    public class OnDeathEventArgs
    {
        private IModelObject destroyer;
        private IModelObject destroyed;

        public OnDeathEventArgs(IModelObject destroyed, IModelObject destroyer)
            : base()
        {
            this.destroyed = destroyed;
            this.destroyer = destroyer;
        }

        public IModelObject DestroyedObject
        {
            get
            {
                return destroyed;
            }
        }

        public IModelObject DestroyingObject
        {
            get
            {
                return destroyer;
            }
        }
    }
}
