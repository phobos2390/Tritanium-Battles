using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using LightGameEngine.Model;
using LightGameEngine.Controller;

namespace LightGameEngine.View
{
    public class View : GameWindow
    {
        private Frustum viewFrustum;

        private Angle camPitch;
        private Angle camYaw;
        private Angle camRoll;

        private Vector3d position;

        private Model.Model model;
        private PhysicsController physics;

        public View(Model.Model model, int height, int width, Frustum viewFrustum, OpenTK.Graphics.GraphicsMode mode, string title)
            : base(height, width, mode, title)
        {
            this.physics = new PhysicsController(model);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);
            GL.DepthFunc(DepthFunction.Lequal);

            this.model = model;
            this.viewFrustum = viewFrustum;
            VSync = VSyncMode.On;
            this.position = Vector3d.Zero;
        }

        public View(Model.Model model, int height, int width, Frustum viewFrustum, string title) 
            :this(model, height, width, viewFrustum, OpenTK.Graphics.GraphicsMode.Default, title) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(System.Drawing.Color.Black);
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

            Matrix4d modelview = Matrix4d.CreateRotationZ((float)camRoll.Radians)
                * Matrix4d.CreateRotationX((float)camYaw.Radians)
                * Matrix4d.CreateRotationY((float)camPitch.Radians)
                * Matrix4d.CreateTranslation(this.position);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            this.physics.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Vector3d lastObjPos = Vector3d.UnitZ;
            foreach (IModelObject obj in this.model.Objects)
            {
                ModelDrawer.Draw(obj);
                lastObjPos = obj.Position;
            }
            Tuple<Angle, Angle> angles = Angle.AngleOfVector(lastObjPos);
            this.camPitch = angles.Item1;
            this.camYaw = angles.Item2;
            SwapBuffers();
        }

        public Vector3d Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        public Angle CamPitch
        {
            get
            {
                return this.camPitch;
            }
            set
            {
                this.camPitch = value;
            }
        }

        public Angle CamYaw
        {
            get
            {
                return this.camYaw;
            }
            set 
            { 
                this.camYaw = value; 
            }
        }

        public Angle CamRoll
        {
            get
            {
                return this.camRoll;
            }
            set
            {
                this.camRoll = value;
            }
        }

    }
}
