using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Data.VertexData;
using ObjLoader.Loader.Data;
using ObjLoader.Loader.Loaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;

namespace LightGameEngine
{
    class ModelDrawer
    {
        private ModelDrawer(){}

        public static void Draw(ModelObject objectToDraw)
        {
            GL.PushMatrix();
            GL.Translate(objectToDraw.Position);
            GL.Rotate(objectToDraw.ZRotation.Degrees, 0, 0, 1);
            GL.Rotate(objectToDraw.XRotation.Degrees, 1, 0, 0);
            GL.Rotate(objectToDraw.YRotation.Degrees, 0, 1, 0);
            foreach (Group g in objectToDraw.Groups)
            {
                GL.Color3(g.Material.DiffuseColor.X,
                        g.Material.DiffuseColor.Y,
                        g.Material.DiffuseColor.Z);
                foreach (Face f in g.Faces)
                {
                    GL.Begin(BeginMode.Polygon);
                    for (int i = 0; i < f.Count; i++)
                    {
                        var vertex = objectToDraw.Vertices[f[i].VertexIndex];
                        GL.Vertex3(vertex.X, vertex.Y, vertex.Z);
                    }
                    GL.End();
                }
            }
            GL.PopMatrix();
        }
    }
}
