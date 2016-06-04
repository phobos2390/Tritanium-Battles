using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine
{
    public interface IGamepadInterface
    {
        void MoveLeftJoystick(Vector2d position);
        void MoveRightJoystick(Vector2d position);
        void PressA();
        void ReleaseA();
        void PressB();
        void ReleaseB();
        void PressX();
        void ReleaseX();
        void PressY();
        void ReleaseY();
        void PressDUp();
        void ReleaseDUp();
        void PressDDown();
        void ReleaseDDown();
        void PressDLeft();
        void ReleaseDLeft();
        void PressDRight();
        void ReleaseDRight();
        void PressShoulderRight();
        void ReleaseShoulderRight();
        void PressShoulderLeft();
        void ReleaseShoulderLeft();
    }
}
