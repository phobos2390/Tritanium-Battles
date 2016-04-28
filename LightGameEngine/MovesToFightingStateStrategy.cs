using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    class MovesToFightingStateStrategy : IPatrollingStateOnSeesOpponentStrategy
    {
        public void OnSeesOpponent(PatrollingState state, IModelObject opponent, IComputerStateMachine machine)
        {
            FightingState newState = new FightingState(state);
            newState.OnSeesOpponent(opponent, machine);
            machine.UpdateState(newState);
        }
    }
}
