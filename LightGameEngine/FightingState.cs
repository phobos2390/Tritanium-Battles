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
        private const double WEAPONCOOLDOWN = 0.25;

        private double timeSinceLastSeen;
        private double coolingDown;
        private IComputerState afterDone;
        private IModelObject opponent;

        public FightingState(IComputerState afterDone)
        {
            this.afterDone = afterDone;
        }

        public void OnSeesOpponent(IModelObject opponent, IComputerStateMachine machine)
        {
            if(coolingDown < 0)
            {
                Console.WriteLine("Firing weapons");
                machine.FireWeapons();
                coolingDown = WEAPONCOOLDOWN;
            }
            this.opponent = opponent;
            timeSinceLastSeen = MAXUNSEENTIME;
        }

        public void OnOpponentDestroyed(IComputerStateMachine machine)
        {
            machine.UpdateState(afterDone);
        }

        public void OnUpdate(FrameEventArgs e, IComputerStateMachine machine)
        {
            coolingDown -= e.Time;
            timeSinceLastSeen -= e.Time;
            if(timeSinceLastSeen <= 0)
            {
                machine.UpdateState(afterDone);
            }
            if(opponent != null)
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
