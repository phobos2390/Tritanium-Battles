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
        private GamePadController gamePadController;
        private IModelObject mainObject;
        private int gamePadIndex;

        public View(int gamePadIndex, Model.Model model, ControllableObject obj, int height, int width, Frustum viewFrustum, OpenTK.Graphics.GraphicsMode mode, string title)
            : base(height, width, mode, title)
        {
            this.mainObject = obj;
            this.physics = new PhysicsController(model);

            this.gamePadIndex = gamePadIndex;
            this.gamePadController = new GamePadController(obj);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);
            GL.DepthFunc(DepthFunction.Lequal);

            this.model = model;
            this.viewFrustum = viewFrustum;
            VSync = VSyncMode.On;
            this.position = Vector3d.Zero;
        }

        public View(int gamePadIndex, Model.Model model, ControllableObject obj, int height, int width, Frustum viewFrustum, string title) 
            :this(gamePadIndex, model, obj, height, width, viewFrustum, OpenTK.Graphics.GraphicsMode.Default, title) { }

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

            this.physics.OnUpdateFrame(e);
            this.gamePadController.OnUpdateState(this.gamePadIndex);

            camYaw = mainObject.Yaw;
            camPitch = mainObject.Pitch;
            camRoll = mainObject.Roll;

            position = mainObject.Position;

            //Matrix4d modelview = Matrix4d.CreateTranslation(-this.position)
            //    * Matrix4d.CreateRotationY(-camPitch.Radians)
            //    * Matrix4d.CreateRotationX(-camYaw.Radians)
            //    * Matrix4d.CreateRotationZ(-camRoll.Radians);

            GL.MatrixMode(MatrixMode.Modelview);
            //GL.LoadMatrix(ref modelview);

            GL.LoadIdentity();
            GL.Rotate(-camPitch.Degrees, 0, 1, 0);
            GL.Rotate(-camYaw.Degrees, 1, 0, 0);
            GL.Rotate(-camRoll.Degrees, 0, 0, 1);
            GL.Translate(-position);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            foreach (IModelObject obj in this.model.Objects)
            {
                //if(mainObject != obj)
                //{
                    ModelDrawer.Draw(obj);
                //}
            }
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
