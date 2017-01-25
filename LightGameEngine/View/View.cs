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
using GeometryLibrary;

namespace LightGameEngine.View
{
    public class View : GameWindow
    {
        private static float Light_X = 0;
        private static float Light_Y = -1;
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
        private double crosshairSize;

        public void OnShipSight(object sender, OnSightEventArgs e)
        {

        }
        
        public View(int gamePadIndex, Model.Model model, ControllableObject obj, int height, int width, Frustum viewFrustum, OpenTK.Graphics.GraphicsMode mode, string title)
            : base(width, height, mode, title)
        {
            this.mainObject = obj;
            this.mainObject.ControlledObject.OnSight += OnShipSight;
            this.physics = new PhysicsController(model);
            
            this.gamePadIndex = gamePadIndex;
            this.gamePadController = new GamePadController(obj);
            this.keyboardController = new KeyboardController(obj);

            this.model = model;
            this.viewFrustum = viewFrustum;
            VSync = VSyncMode.On;
            this.position = Vector3d.Zero;

            crosshairSize = .125;

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
            GL.Enable(EnableCap.LineSmooth);

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
            if (ClientRectangle.Height > 0)
            {
                viewFrustum.Aspect = ClientRectangle.Width / ClientRectangle.Height;
            }
            Matrix4d viewMatrix = viewFrustum.Matrix;

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref viewMatrix);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            this.physics.OnUpdateFrame(e);
            this.gamePadController.OnUpdateState(this.gamePadIndex,this);
            this.keyboardController.OnUpdateState(this);

            camOrientation = mainObject.Orientation;

            position = mainObject.Position;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (this.mainObject.Destroyed)
            {
                QFont.Begin();
                font.Print("Game Over", new Vector2((float).8 * ClientRectangle.Width /2, (float).8 * ClientRectangle.Height /2));
                QFont.End();
            }
            else if (this.model.Objects.Count <= 1)
            {
                QFont.Begin();
                font.Print("Last Person Standing", new Vector2((float).8 * ClientRectangle.Width / 2, (float).8 * ClientRectangle.Height / 2));
                QFont.End();
            }
            else
            {
                GL.MatrixMode(MatrixMode.Modelview);

                GL.LoadIdentity();
                camOrientation.Invert();
                Vector3d rotationAxis = new Vector3d();
                double rotationAngle = 0;
                camOrientation.ToAxisAngle(out rotationAxis, out rotationAngle);
                Angle rotation = Angle.CreateRadian(rotationAngle);

                GL.PushMatrix();

                GL.Rotate(rotation.Degrees, rotationAxis);
                GL.Translate(-position);

                foreach (IModelObject obj in this.model.Objects)
                {
                    ModelDrawer.Draw(obj);
                }

                GL.PopMatrix();

                GL.Begin(PrimitiveType.Lines);
                GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
                GL.Color3(Color.Yellow);
                GL.LineWidth(6);
                Vector3d initialPoint = new Vector3d(0, 0, -2);
                Vector3d finalPoint = Vector3d.Transform(mainObject.Velocity, camOrientation);
                GL.Vertex3(initialPoint);
                GL.Vertex3(initialPoint + finalPoint.Normalized());
                GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
                GL.Color3(Color.Red);
                GL.Vertex3(initialPoint + finalPoint.Normalized());
                GL.Vertex3(initialPoint + finalPoint.Normalized() * 1.25);

                Vector3d accelVector = Vector3d.Transform(-Vector3d.UnitZ, camOrientation);

                GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
                var intersectVals = model.IntersectScene(position, accelVector, mainObject);
                if(intersectVals.Item1 != null)
                {
                    GL.Color3(Color.Green);
                }
                else
                {
                    GL.Color3(Color.Blue);
                }

                GL.Vertex3(new Vector3d(4 * crosshairSize, 0, -2));
                GL.Vertex3(new Vector3d(2 * crosshairSize, 0, -2));

                GL.Vertex3(new Vector3d(-4 * crosshairSize, 0, -2));
                GL.Vertex3(new Vector3d(-2 * crosshairSize, 0, -2));

                GL.Vertex3(new Vector3d(0, 4 * crosshairSize, -2));
                GL.Vertex3(new Vector3d(0, 2 * crosshairSize, -2));

                GL.Vertex3(new Vector3d(0, -4 * crosshairSize, -2));
                GL.Vertex3(new Vector3d(0, -2 * crosshairSize, -2));

                GL.Vertex3(new Vector3d(crosshairSize, crosshairSize, -2));
                GL.Vertex3(new Vector3d(-crosshairSize, -crosshairSize, -2));

                GL.Vertex3(new Vector3d(crosshairSize, -crosshairSize, -2));
                GL.Vertex3(new Vector3d(-crosshairSize, crosshairSize, -2));

                GL.End();

                QFont.Begin();
                font.Print("Missiles ( " + mainObject.LeftMissiles + "," + mainObject.RightMissiles + " ) ", new Vector2(0, 0));
                font.Print("High Missiles ( " + mainObject.LeftHighMissiles + "," + mainObject.RightHighMissiles + " ) ", new Vector2(0, 25));
                font.Print("Fire Mode: " + mainObject.FireMode, new Vector2(0, 50));
                font.Print("Fire Type: " + mainObject.MissileType, new Vector2(0, 75));
                font.Print("Velocity: (" + (int)mainObject.Velocity.Length + ")", new Vector2(0, 100));
                font.Print("Position: (" + (int)mainObject.Position.X + "," + (int)mainObject.Position.Y + "," + (int)mainObject.Position.Z + ")", new Vector2(0, 125));
                QFont.End();

                GL.Disable(EnableCap.Texture2D);
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

        public void ZoomIn()
        {
            Console.WriteLine("Zooming in");
            Angle fovy = viewFrustum.FofViewY;
            if (fovy.Degrees > .5)
            {
                fovy.Divide(1.25);
            }
            else
            {
                fovy.Degrees = .5;
            }
            viewFrustum.FofViewY = fovy;
            refreshViewMatrix();
        }

        public void ZoomOut()
        {
            Console.WriteLine("Zooming Out");
            Angle fovy = viewFrustum.FofViewY;
            if (fovy.Degrees < 90)
            {
                fovy.Multiply(1.25);
            }
            else
            {
                fovy.Degrees = 90;
            }
            viewFrustum.FofViewY = fovy;
            refreshViewMatrix();
        }

        private void refreshViewMatrix()
        {
            Matrix4d viewMatrix = viewFrustum.Matrix;

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref viewMatrix);
        }
    }
}
