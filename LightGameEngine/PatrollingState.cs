using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightGameEngine.Model;
using LightGameEngine.Collision;

namespace LightGameEngine
{
    public class PatrollingState : IComputerState
    {
        private const int MAXWPTDISTSQUARED = 1;

        private IList<Vector3d> wayPoints;
        private int wayPointIter;
        private IComputerState fightingState;
        private IComputerStateMachine machine;

        public PatrollingState(IList<Vector3d> wayPoints, IComputerStateMachine machine, IComputerState fightingState)
        {
            this.wayPoints = wayPoints;
            this.fightingState = fightingState;
            this.machine = machine;
            this.wayPointIter = 0;
        }

        public IComputerState FightingState
        {
            set
            {
                this.fightingState = value;
            }
        }

        public IComputerStateMachine Machine
        {
            set
            {
                this.machine = value;
            }
        }

        public void OnSeesOpponent(IModelObject opponent)
        {
            if(fightingState != this)
            {
                fightingState.OnSeesOpponent(opponent);
            }
            machine.UpdateState(fightingState);
        }

        public void OnUpdate(FrameEventArgs e)
        {
            if(Sphere.PointInSphere(wayPoints[wayPointIter],machine.Position, MAXWPTDISTSQUARED))
            {
                wayPointIter = (wayPointIter + 1) % wayPoints.Count;
            }
            machine.TurnTowardsPoint(wayPoints[wayPointIter], e.Time);
        }
    }
}
