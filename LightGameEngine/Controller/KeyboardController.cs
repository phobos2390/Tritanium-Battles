using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using OpenTK;

namespace LightGameEngine
{
    public class KeyboardController
    {
        private KeyboardState old_state;
        private IGamepadInterface controlled;

        public KeyboardController(IGamepadInterface controlled)
        {
            this.controlled = controlled;
        }

        public void OnUpdateState()
        {
            KeyboardState state = Keyboard.GetState();
            if (!state.Equals(old_state))
            {
                if (state[Key.K] && !old_state[Key.K])
                {
                    controlled.PressA();
                }
                if (!state[Key.K] && old_state[Key.K])
                {
                    controlled.ReleaseA();
                }
                if (state[Key.L] && !old_state[Key.L])
                {
                    controlled.PressB();
                }
                if (!state[Key.L] && old_state[Key.L])
                {
                    controlled.ReleaseB();
                }
                if (state[Key.J] && !old_state[Key.J])
                {
                    controlled.PressX();
                }
                if (!state[Key.J] && old_state[Key.J])
                {
                    controlled.ReleaseX();
                }
                if (state[Key.J] && !old_state[Key.J])
                {
                    controlled.PressY();
                }
                if (!state[Key.J] && old_state[Key.J])
                {
                    controlled.ReleaseY();
                }
                // Update state for the next frame
                old_state = state;
            }
            if (state[Key.Q])
            {
                controlled.MoveLeftJoystick(-Vector2d.UnitX);
            }
            if(state[Key.E])
            {
                controlled.MoveLeftJoystick(Vector2d.UnitX);
            }
            if(state[Key.W])
            {
                controlled.MoveLeftJoystick(Vector2d.UnitY);
            }
            if(state[Key.S])
            {
                controlled.MoveLeftJoystick(-Vector2d.UnitY);
            }
            if(state[Key.A])
            {
                controlled.MoveRightJoystick(-Vector2d.UnitX);
            }
            if (state[Key.D])
            {
                controlled.MoveRightJoystick(Vector2d.UnitX);
            }
        }
    }
}
