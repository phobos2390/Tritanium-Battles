﻿using ObjLoader.Loader.Data.Elements;
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
        void Destroy();
        bool Destroyed { get; }
        Vector3d Position { get; set; }
        Vector3d Velocity { get; set; }
        Quaterniond Orientation { get; set; }
        IList<Group> Groups { get; }
        IList<Vertex> Vertices { get; }
        IList<Normal> Normals { get; }
        bool EqualsOtherObject(IModelObject other);
    }
}