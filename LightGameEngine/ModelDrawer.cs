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

namespace LightGameEngine.Model
{
    class ModelDrawer
    {
        private ModelDrawer(){}

        public static void Draw(IModelObject objectToDraw)
        {
            GL.PushMatrix();
            GL.Translate(objectToDraw.Position);
            GL.Rotate(objectToDraw.Roll.Degrees, 0, 0, 1);
            GL.Rotate(objectToDraw.Yaw.Degrees, 1, 0, 0);
            GL.Rotate(objectToDraw.Pitch.Degrees, 0, 1, 0);
            foreach (Group g in objectToDraw.Groups)
            {
                foreach (Face f in g.Faces)
                {
                    GL.Begin(PrimitiveType.Polygon);
                    GL.Color3(g.Material.DiffuseColor.X,
                       g.Material.DiffuseColor.Y,
                       g.Material.DiffuseColor.Z);
                    for (int i = 0; i < f.Count; i++)
                    {
                        var vertex = objectToDraw.Vertices[f[i].VertexIndex - 1];
                        GL.Vertex3(vertex.X, vertex.Y, vertex.Z);
                    }
                    GL.End();
                }
            }
            GL.PopMatrix();
        }
    }
}
