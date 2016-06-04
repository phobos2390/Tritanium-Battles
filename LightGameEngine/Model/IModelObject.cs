using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Data.VertexData;
using OpenTK;
using System.Collections.Generic;

namespace LightGameEngine.Model
{
    public delegate void OnDeathHandler(object sender, OnDeathEventArgs e);

    public interface IModelObject
    {
        event OnDeathHandler OnDeath;
        void OnUpdate(FrameEventArgs e);
        void AddForce(Vector3d force);
        double Mass { get; }
        double Scale {get;}
        void Destroy(IModelObject destroyer);
        bool Destroyed { get; }
        Vector3d Position { get; set; }
        Vector3d Velocity { get; set; }
        Quaterniond Orientation { get; set; }
        IList<Group> Groups { get; }
        IList<Vertex> Vertices { get; }
        IList<Normal> Normals { get; }
        double RadiusSquared { get; }
        bool EqualsOtherObject(IModelObject other);
    }
}