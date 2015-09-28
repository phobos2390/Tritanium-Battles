using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace LightGameEngine.View
{
    public class View : GameWindow
    {
        private Frustum viewFrustum;

        private Angle camPitch;
        private Angle camYaw;
        private Angle camRoll;

        private Vector3 position;

        public View(int height, int width, Frustum viewFrustum, OpenTK.Graphics.GraphicsMode mode, string title)
            : base(height, width, mode, title)
        {
            this.viewFrustum = viewFrustum;
            VSync = VSyncMode.On;
        }

        public View(int height, int width, Frustum viewFrustum, string title) 
            :this(height, width, viewFrustum, OpenTK.Graphics.GraphicsMode.Default, title) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

            Matrix4d viewMatrix = viewFrustum.Matrix;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref viewMatrix);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 modelview = Matrix4.CreateRotationZ((float)camRoll.Radians)
                * Matrix4.CreateRotationX((float)camYaw.Radians)
                * Matrix4.CreateRotationY((float)camPitch.Radians)
                * Matrix4.CreateTranslation(this.position);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            SwapBuffers();
        }
    }
}
