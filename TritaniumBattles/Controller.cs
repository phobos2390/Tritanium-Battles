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
        private static double UPS = 60;
        private static double RPS = 60;
        static double HEIGHT = 650;
        static double WIDTH = 650;
        static double NEAR = 1;
        static double FAR = 1000;
        static double FOVY = 90;
        static string TITLE = "Tritanium Battles";
        static void Main(string[] args)
        {
            ModelObjectFactory fact = new ModelObjectFactory();
            Model model = new Model();
            model.AddModelObject(ModelObjectFactory.CreateModel(ModelTypes.Asteroid, 0,0,0, new Vector3d(0,10,10)));
            model.AddModelObject(ModelObjectFactory.CreateModel(ModelTypes.Asteroid, 0, 0, 0, new Vector3d(0, 10, -10)));
            model.AddModelObject(ModelObjectFactory.CreateModel(ModelTypes.Asteroid, 0, 0, 0, new Vector3d(0, 0, 10)));
            model.AddModelObject(ModelObjectFactory.CreateModel(ModelTypes.Asteroid, 0, 0, 0, new Vector3d(0, 0, -10)));
            model.AddModelObject(ModelObjectFactory.CreateModel(ModelTypes.Asteroid, 0, 0, 0, new Vector3d(0, 10, 0)));
            model.AddModelObject(ModelObjectFactory.CreateModel(ModelTypes.Asteroid, 0, 0, 0, new Vector3d(0,-10, 0)));

            model.AddModelObject(ModelObjectFactory.CreateModel(ModelTypes.Asteroid, 0, 0, 0, new Vector3d(10, 0, 0)));
            model.AddModelObject(ModelObjectFactory.CreateModel(ModelTypes.Asteroid, 0, 0, 0, new Vector3d(-10, 0, 0)));
            model.AddModelObject(ModelObjectFactory.CreateModel(ModelTypes.Asteroid, 0, 0, 0, new Vector3d(0, 50, 10)));
            model.AddModelObject(ModelObjectFactory.CreateModel(ModelTypes.Asteroid, 0, 0, 0, new Vector3d(0, 10, -50)));
            model.AddModelObject(ModelObjectFactory.CreateModel(ModelTypes.Asteroid, 0, 0, 0, new Vector3d(5, 0, 10)));
            model.AddModelObject(ModelObjectFactory.CreateModel(ModelTypes.Asteroid, 0, 0, 0, new Vector3d(5, 0, -10)));
            model.AddModelObject(ModelObjectFactory.CreateModel(ModelTypes.MSFV5, 0, 0, 0, new Vector3d(10, 0, 10)));
            model.AddModelObject(ModelObjectFactory.CreateModel(ModelTypes.MSFV3, 0, 0, 0, new Vector3d(10, 5, 10)));
            model.AddModelObject(ModelObjectFactory.CreateModel(ModelTypes.Carpo, 0, 0, 0, new Vector3d(10, 10, 10)));
            ControllableObject obj = fact.CreateControlledObject(ModelTypes.MSFV3, model, ModelTypes.Missile);
            model.AddModelObject(obj);
            View view = new View(0,model, obj, (int)HEIGHT, (int)WIDTH, new Frustum(Angle.CreateDegree(FOVY),(double)WIDTH / HEIGHT,NEAR,FAR), TITLE);
            Console.WriteLine("Loading done.");
            view.Run(UPS, RPS);
            Console.WriteLine("Everything done.");
        }
    }
}
