using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Data.VertexData;
using OpenTK;
using System.Collections.Generic;

namespace LightGameEngine.Model
{
    public interface IModelObject
    {
        void OnUpdate(FrameEventArgs e);
        void AddForce(Vector3d force);
        double Mass { get; }
        Vector3d Position { get; set; }
        Angle Pitch { get; set; }
        Angle Yaw { get; set; }
        Angle Roll { get; set; }
        IList<Group> Groups { get; }
        IList<Vertex> Vertices { get; }
    }
}