using LightGameEngine.Model;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine
{
    public interface IComputerState
    {
        void OnUpdate(FrameEventArgs e, IComputerStateMachine machine);
        void OnSeesOpponent(IModelObject opponent, IComputerStateMachine machine);
    }
}
