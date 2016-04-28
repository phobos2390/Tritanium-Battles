using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public interface IPatrollingStateOnSeesOpponentStrategy
    {
        void OnSeesOpponent(PatrollingState state, IModelObject opponent, IComputerStateMachine machine);
    }
}
