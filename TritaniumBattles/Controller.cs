using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LightGameEngine;
using LightGameEngine.View;
using LightGameEngine.Model;
using OpenTK;
using ObjLoader.Loader;

namespace TritaniumBattles
{
    class Controller
    {
        private static string MSFV3 = "MSFV3LowerPoly.obj";
        private static string MSFV5 = "MartianSpaceFighterVersion5.obj";
        private static string CARPO = "CarpoClass.obj";
        private static double UPS = 60;
        private static double RPS = 60;
        static double HEIGHT = 500;
        static double WIDTH = 500;
        static double NEAR = 2;
        static double FAR = 1000;
        static double FOVY = 90;
        static string TITLE = "Tritanium Battles";
        static void Main(string[] args)
        {
            ModelObject modObject = ModelObjectFactory.LoadFromFile("MSFV3LowerPoly.obj");
            modObject.AddForce(new Vector3d(0, 0, -2/UPS));
            Model model = new Model();
            IModelObject propelledObject = new PropelledObject(-6 / UPS, modObject);
            model.AddModelObject(propelledObject);
            //model.AddModelObject(modObject);
            View view = new View(model, (int)HEIGHT, (int)WIDTH, new Frustum(Angle.CreateDegree(FOVY),(double)WIDTH / HEIGHT,NEAR,FAR), TITLE);
            Console.WriteLine("Loading done.");
            view.Run(UPS, RPS);
            Console.WriteLine("Everything done.");
        }
    }
}
