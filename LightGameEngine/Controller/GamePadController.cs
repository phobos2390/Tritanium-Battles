using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightGameEngine.Model;
using OpenTK;

namespace LightGameEngine.Controller
{
    public class GamePadController
    {
        private GamePadState old_state;
        private IGamepadInterface controlled;

        public GamePadController(IGamepadInterface controlled)
        {
            this.controlled = controlled;
        }
        
        public void OnUpdateState(int index, View.View view)
        {
            GamePadState state = GamePad.GetState(index);
            if (!state.Equals(old_state))
            {
                if (state.Buttons.A == ButtonState.Pressed && old_state.Buttons.A != ButtonState.Pressed)
                {
                    controlled.PressA();
                }
                if(state.Buttons.A != ButtonState.Pressed && old_state.Buttons.A == ButtonState.Pressed)
                {
                    controlled.ReleaseA();
                }
                if (state.Buttons.B == ButtonState.Pressed && old_state.Buttons.B != ButtonState.Pressed)
                {
                    controlled.PressB();
                }
                if (state.Buttons.B != ButtonState.Pressed && old_state.Buttons.B == ButtonState.Pressed)
                {
                    controlled.ReleaseB();
                }
                if (state.Buttons.X == ButtonState.Pressed && old_state.Buttons.X != ButtonState.Pressed)
                {
                    controlled.PressX();
                }
                if (state.Buttons.X != ButtonState.Pressed && old_state.Buttons.X == ButtonState.Pressed)
                {
                    controlled.ReleaseX();
                }
                if (state.Buttons.Y == ButtonState.Pressed && old_state.Buttons.Y != ButtonState.Pressed)
                {
                    controlled.PressY();
                }
                if (state.Buttons.Y != ButtonState.Pressed && old_state.Buttons.Y == ButtonState.Pressed)
                {
                    controlled.ReleaseY();
                }
                if (state.Buttons.LeftShoulder == ButtonState.Pressed && old_state.Buttons.LeftShoulder != ButtonState.Pressed)
                {
//                    controlled.PressShoulderLeft();
                    view.ZoomOut();
                }
                if (state.Buttons.LeftShoulder != ButtonState.Pressed && old_state.Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    controlled.ReleaseShoulderLeft();
                }
                if (state.Buttons.RightShoulder == ButtonState.Pressed && old_state.Buttons.RightShoulder != ButtonState.Pressed)
                {
                    //                    controlled.PressShoulderRight();
                    view.ZoomIn();
                }
                if (state.Buttons.RightShoulder != ButtonState.Pressed && old_state.Buttons.RightShoulder == ButtonState.Pressed)
                {
                    controlled.PressShoulderRight();
                }
                // Update state for the next frame
                old_state = state;
            }
            controlled.MoveLeftJoystick((Vector2d)state.ThumbSticks.Left);
            controlled.MoveRightJoystick((Vector2d)state.ThumbSticks.Right);
        }
    }
}
