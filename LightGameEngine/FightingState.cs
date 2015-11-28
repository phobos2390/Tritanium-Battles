using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightGameEngine.Model;
using OpenTK;

namespace LightGameEngine
{
    public class FightingState : IComputerState
    {
        private const double MAXUNSEENTIME = 10;

        private double timeSinceLastSeen;
        private IComputerState afterDone;
        private IComputerStateMachine machine;
        private IModelObject opponent;

        public IComputerStateMachine Machine
        {
            set
            {
                this.machine = value;
            }
        }

        public FightingState(IComputerStateMachine machine, IComputerState afterDone)
        {
            this.machine = machine;
            this.afterDone = afterDone;
        }

        public void OnSeesOpponent(IModelObject opponent)
        {
            machine.FireWeapons();
            this.opponent = opponent;
            timeSinceLastSeen = MAXUNSEENTIME;
        }

        public void OnOpponentDestroyed()
        {
            machine.UpdateState(afterDone);
        }

        public void OnUpdate(FrameEventArgs e)
        {
            timeSinceLastSeen -= e.Time;
            if(timeSinceLastSeen <= 0)
            {
                machine.UpdateState(afterDone);
            }
            if(this.opponent != null)
            {
                machine.TurnTowardsPoint(this.opponent.Position, e.Time);
            }
            else
            {
                machine.UpdateState(afterDone);
            }
        }
    }
}
