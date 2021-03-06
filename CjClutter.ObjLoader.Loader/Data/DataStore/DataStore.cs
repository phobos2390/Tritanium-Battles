﻿using System.Collections.Generic;
using System.Linq;
using ObjLoader.Loader.Common;
using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Data.VertexData;
using System.IO;
using System;

namespace ObjLoader.Loader.Data.DataStore
{
    public class DataStore : IDataStore, IGroupDataStore, IVertexDataStore, ITextureDataStore, INormalDataStore,
                             IFaceGroup, IMaterialLibrary, IElementGroup
    {
        private Group _currentGroup;

        private readonly List<Group> _groups = new List<Group>();
        private readonly List<Material> _materials = new List<Material>();
        private readonly List<Vertex> _vertices = new List<Vertex>();
        private readonly List<Texture> _textures = new List<Texture>();
        private readonly List<Normal> _normals = new List<Normal>();
        private double radiusSquared;

        public DataStore()
        {
            radiusSquared = 0;
        }
        
        public IList<Vertex> Vertices
        {
            get { return _vertices; }
        }

        public IList<Texture> Textures
        {
            get { return _textures; }
        }

        public IList<Normal> Normals
        {
            get { return _normals; }
        }

        public IList<Material> Materials
        {
            get { return _materials; }
        }

        public IList<Group> Groups
        {
            get { return _groups; }
        }

        public double RadiusSquared
        {
            get { return this.radiusSquared; }
        }

        public void AddFace(Face face)
        {
            PushGroupIfNeeded();

            _currentGroup.AddFace(face);
        }

        public void PushGroup(string groupName)
        {
            Material mat = null;
            if (_currentGroup != null && _currentGroup.Material != null)
            {
                mat = _currentGroup.Material;
            }
            _currentGroup = new Group(groupName);
            _currentGroup.Material = mat;
            _groups.Add(_currentGroup);
        }

        private void PushGroupIfNeeded()
        {
            if (_currentGroup == null)
            {
                PushGroup("default");
            }
        }

        public void AddVertex(Vertex vertex)
        {
            double newRadSquare = vertex.X * vertex.X + vertex.Y * vertex.Y + vertex.Z * vertex.Z;
            if(newRadSquare > this.radiusSquared)
            {
                this.radiusSquared = newRadSquare;
            }
            _vertices.Add(vertex);
        }

        public void AddTexture(Texture texture)
        {
            _textures.Add(texture);
        }

        public void AddNormal(Normal normal)
        {
            _normals.Add(normal);
        }

        public void Push(Material material)
        {
            _materials.Add(material);
        }

        public void SetMaterial(string materialName)
        {
            var material = _materials.SingleOrDefault(x => x.Name.EqualsInvariantCultureIgnoreCase(materialName));
            PushGroup(materialName);
            _currentGroup.Material = material;
        }
    }
}