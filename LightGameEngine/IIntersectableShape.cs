using System;
using OpenTK;

namespace LightGameEngine.Collision
{
    public interface IIntersectableShape
    {
        Tuple<bool, Vector3d, Vector3d> Intersects(Vector3d initial, Vector3d ray);
    }
}