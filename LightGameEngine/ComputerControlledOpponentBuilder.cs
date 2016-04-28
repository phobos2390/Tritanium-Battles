using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public class ComputerControlledOpponentBuilder
    {
        IComputerState initial;
        ShipObject ship;

        public ComputerControlledOpponentBuilder SetInitialState(IComputerState state)
        {
            initial = state;
            return this;
        }

        public ComputerControlledOpponentBuilder SetShip(ShipObject ship)
        {
            this.ship = ship;
            return this;
        }

        public ComputerControlledOpponent CreateAI()
        {
            return new ComputerControlledOpponent(ship, initial);
        }
    }
}
