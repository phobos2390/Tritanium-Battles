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
        private ModelDrawer()
        {
        }

        public static void Draw(IModelObject objectToDraw)
        {
            GL.PushMatrix();
            GL.Translate(objectToDraw.Position);
            GL.Rotate(objectToDraw.Yaw.Degrees, 1, 0, 0);
            GL.Rotate(objectToDraw.Pitch.Degrees, 0, 1, 0);
            GL.Rotate(objectToDraw.Roll.Degrees, 0, 0, 1);
            foreach (Group g in objectToDraw.Groups)
            {
                foreach (Face f in g.Faces)
                {
                    GL.Begin(PrimitiveType.Polygon);
                    GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.Ambient);
                    GL.Color3(g.Material.AmbientColor.X, 
                        g.Material.AmbientColor.Y, 
                        g.Material.AmbientColor.Z);
                    GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.Diffuse);
                    GL.Color3(g.Material.DiffuseColor.X,
                       g.Material.DiffuseColor.Y,
                       g.Material.DiffuseColor.Z);
                    GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.Specular);
                    GL.Color3(g.Material.SpecularColor.X,
                       g.Material.SpecularColor.Y,
                       g.Material.SpecularColor.Z);
                    GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.Emission);
                    for (int i = 0; i < f.Count; i++)
                    {
                        var vertex = objectToDraw.Vertices[f[i].VertexIndex - 1];
                        var normal = objectToDraw.Normals[f[i].NormalIndex - 1];
                        GL.Normal3(normal.X, normal.Y, normal.Z);
                        GL.Vertex3(vertex.X, vertex.Y, vertex.Z);
                    }
                    GL.End();
                }
            }
            GL.PopMatrix();
        }
    }
}
