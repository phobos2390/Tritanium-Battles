using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public class ComputerStateMachineBuilder
    {
        IList<Vector3d> waypoints;
        IPatrollingStateOnSeesOpponentStrategy strategy;

        public ComputerStateMachineBuilder()
        {
            waypoints = new List<Vector3d>();
        }

        public ComputerStateMachineBuilder SetWaypoints(IList<Vector3d> way)
        {
            waypoints = way;
            return this;
        }

        public ComputerStateMachineBuilder AddWayPoint(Vector3d waypoint)
        {
            waypoints.Add(waypoint);
            return this;
        }

        public ComputerStateMachineBuilder SetAIToFighter()
        {
            strategy = new MovesToFightingStateStrategy();
            return this;
        }

        public ComputerStateMachineBuilder SetAIToPeaceFull()
        {
            strategy = new StaysInPatrollingStateStrategy();
            return this;
        }

        public IComputerState CreateStates()
        {
            return new PatrollingState(waypoints, strategy);
        }
    }
}
