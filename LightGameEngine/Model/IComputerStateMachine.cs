using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine
{
    public interface IComputerStateMachine
    {
        Vector3d Position
        {
            get;
        }
        void UpdateState(IComputerState newState);
        void FireWeapons();
        void FireEngines();
        void TurnTowardsPoint(Vector3d point, double time);
    }
}
