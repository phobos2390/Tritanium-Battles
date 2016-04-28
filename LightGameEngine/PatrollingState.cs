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
        private IPatrollingStateOnSeesOpponentStrategy onSeesOpponentStrategy;

        public PatrollingState(IList<Vector3d> wayPoints,IPatrollingStateOnSeesOpponentStrategy onSeesOpponentStrategy)
        {
            this.wayPoints = wayPoints;
            wayPointIter = 0;
            this.onSeesOpponentStrategy = onSeesOpponentStrategy;
        }

        public void OnSeesOpponent(IModelObject opponent, IComputerStateMachine machine)
        {
            onSeesOpponentStrategy.OnSeesOpponent(this, opponent, machine);
        }

        public void OnUpdate(FrameEventArgs e, IComputerStateMachine machine)
        {
            if(Sphere.PointInSphere(wayPoints[wayPointIter],machine.Position, MAXWPTDISTSQUARED))
            {
                wayPointIter = (wayPointIter + 1) % wayPoints.Count;
            }
            machine.TurnTowardsPoint(wayPoints[wayPointIter], e.Time);
        }
    }
}
