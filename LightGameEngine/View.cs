using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using LightGameEngine.Model;
using LightGameEngine.Controller;
using QuickFont;
using System.Drawing;

namespace LightGameEngine.View
{
    public class View : GameWindow
    {
        private static float Light_X = 0;
        private static float Light_Y = 1;
        private static float Light_Z = 0;

        private QFont font;

        private  QFont initFont()
        {
            Font f = new Font(FontFamily.GenericSansSerif, 20);
            QFont rQf = new QFont(f);
            return rQf;
        }

        private Frustum viewFrustum;

        private Quaterniond camOrientation;

        private Vector3d position;

        private Model.Model model;
        private PhysicsController physics;
        private GamePadController gamePadController;
        private KeyboardController keyboardController;
        private ControllableObject mainObject;
        private int gamePadIndex;

        public View(int gamePadIndex, Model.Model model, ControllableObject obj, int height, int width, Frustum viewFrustum, OpenTK.Graphics.GraphicsMode mode, string title)
            : base(width, height, mode, title)
        {
            this.mainObject = obj;
            this.physics = new PhysicsController(model);
            
            this.gamePadIndex = gamePadIndex;
            this.gamePadController = new GamePadController(obj);
            this.keyboardController = new KeyboardController(obj);

            this.model = model;
            this.viewFrustum = viewFrustum;
            VSync = VSyncMode.On;
            this.position = Vector3d.Zero;

            font = initFont();
        }

        public View(int gamePadIndex, Model.Model model, ControllableObject obj, int height, int width, Frustum viewFrustum, string title) 
            :this(gamePadIndex, model, obj, height, width, viewFrustum, OpenTK.Graphics.GraphicsMode.Default, title) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.PolygonSmooth);

            Vector4 lightPos = new Vector4(Light_X, Light_Y, Light_Z, 0);

            GL.Light(LightName.Light0, LightParameter.Position, lightPos);
            GL.Light(LightName.Light0, LightParameter.Diffuse, OpenTK.Graphics.Color4.White);
            GL.Light(LightName.Light0, LightParameter.Specular, OpenTK.Graphics.Color4.White);            

            GL.Enable(EnableCap.ColorMaterial);

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);

            GL.ClearColor(System.Drawing.Color.Black);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);
            GL.DepthFunc(DepthFunction.Lequal);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Enable(EnableCap.PolygonSmooth);

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
            this.keyboardController.OnUpdateState();

            camOrientation = mainObject.Orientation;

            position = mainObject.Position;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            GL.MatrixMode(MatrixMode.Modelview);
            //GL.LoadMatrix(ref modelview);

            GL.LoadIdentity();
            camOrientation.Invert();
            Vector3d rotationAxis = new Vector3d();
            double rotationAngle = 0;
            camOrientation.ToAxisAngle(out rotationAxis, out rotationAngle);
            Angle rotation = Angle.CreateRadian(rotationAngle);
            GL.Rotate(rotation.Degrees, rotationAxis);
            GL.Translate(-position);

            foreach (IModelObject obj in this.model.Objects)
            {
                //if(mainObject != obj)
                //{
                    ModelDrawer.Draw(obj);
                //}
            }

            QFont.Begin();
            font.Print("Missiles ( " + mainObject.LeftMissiles + "," + mainObject.RightMissiles + " ) ", new Vector2(0, 0));
            font.Print("High Missiles ( " + mainObject.LeftHighMissiles + "," + mainObject.RightHighMissiles + " ) ", new Vector2(0, 25));
            font.Print("Fire Mode: " + mainObject.FireMode, new Vector2(0,50));
            font.Print("Fire Type: " + mainObject.MissileType, new Vector2(0, 75));
            font.Print("Velocity: (" + (int)mainObject.Velocity.X + "," + (int)mainObject.Velocity.Y + "," + (int)mainObject.Velocity.Z + ")", new Vector2(0, 100));
            font.Print("Position: (" + (int)mainObject.Position.X + "," + (int)mainObject.Position.Y + "," + (int)mainObject.Position.Z + ")", new Vector2(0, 125));
            QFont.End();

            GL.Disable(EnableCap.Texture2D);
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
    }
}
