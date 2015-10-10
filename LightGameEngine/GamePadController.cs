using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Controller
{
    public class GamePadController
    {
        GamePadState old_state;
        
        void OnUpdateState(int index)
        {
            GamePadState state = GamePad.GetState(index);
            if (!state.Equals(old_state))
            {
                if (state.Buttons.A == ButtonState.Pressed && old_state.Buttons.A == ButtonState.Pressed)
                    ; // Button A is held
                
                // Update state for the next frame
                old_state = state;
            }
        }
    }
}
