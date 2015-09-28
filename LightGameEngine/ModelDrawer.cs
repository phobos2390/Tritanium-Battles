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
        private int textureId;
        private ModelObject objectToDraw;

        public ModelDrawer(Image image, ModelObject objectToDraw)
        {
            GL.BindTexture(TextureTarget.Texture2D, textureId);
            Bitmap bmp = new Bitmap(image);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, image.Height, image.Width, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.Bitmap, data.Scan0);
            GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, Color.Gray);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
        }

        public void Draw()
        {
            GL.BindTexture(TextureTarget.Texture2D, this.textureId);
            GL.PushMatrix();
            GL.Translate(this.objectToDraw.Position);
            GL.Rotate(this.objectToDraw.ZRotation.Degrees, 0, 0, 1);
            GL.Rotate(this.objectToDraw.XRotation.Degrees, 1, 0, 0);
            GL.Rotate(this.objectToDraw.YRotation.Degrees, 0, 1, 0);
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
                        var uv = objectToDraw.Textures[f[i].TextureIndex];
                        GL.TexCoord2(uv.X, uv.Y);
                        GL.Vertex3(vertex.X, vertex.Y, vertex.Z);
                    }
                    GL.End();
                }
            }
            GL.PopMatrix();
        }
    }
}
