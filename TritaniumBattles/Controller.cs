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
using Newtonsoft.Json;
using GeometryLibrary;

namespace TritaniumBattles
{
    class Controller
    {
        private static double UPS = 60;
        private static double RPS = 60;
        static double HEIGHT = 650;
        static double WIDTH = 1000;
        static double NEAR = 1;
        static double FAR = 20000;
        static double FOVY = 90;
        static string TITLE = "Tritanium Battles";
        
        public static void Main(string[] args)
        {
            ModelObjectFactory fact = new ModelObjectFactory();
            Model model = new Model();
            model.AddModelObject(fact.CreateAsteroid(Quaterniond.Identity, new Vector3d(0, 0, -1000)));
            Console.WriteLine("Added an asteroid");
            //model.AddModelObject(fact.CreateUnarmedAI(ModelTypes.Carpo, model, Quaterniond.Identity, ModelObjectFactory.randVec()));
            //model.AddModelObject(fact.CreateAIOpponent(ModelTypes.MSFV3, model, Quaterniond.Identity, ModelObjectFactory.randVec()));
            //Console.WriteLine("Added MSFV3");
            //model.AddModelObject(fact.CreateAIOpponent(ModelTypes.MSFV3, model, Quaterniond.Identity, ModelObjectFactory.randVec()));
            //Console.WriteLine("Added MSFV3");
            //model.AddModelObject(fact.CreateAIOpponent(ModelTypes.MSFV3, model, Quaterniond.Identity, ModelObjectFactory.randVec()));
            //Console.WriteLine("Added MSFV3");
            //model.AddModelObject(fact.CreateAIOpponent(ModelTypes.MSFV3, model, Quaterniond.Identity, ModelObjectFactory.randVec()));
            //Console.WriteLine("Added MSFV3");
            //model.AddModelObject(fact.CreateAIOpponent(ModelTypes.MSFV3, model, Quaterniond.Identity, ModelObjectFactory.randVec()));
            //Console.WriteLine("Added MSFV3");
            //model.AddModelObject(fact.CreateAIOpponent(ModelTypes.MSFV5, model, Quaterniond.Identity, ModelObjectFactory.randVec()));
            //Console.WriteLine("Added MSFV5");
            model.AddModelObject(fact.CreateAIOpponent("CarpoRefit.json", model, Quaterniond.Identity, ModelObjectFactory.randVec()));
            Console.WriteLine("Added CarpoRefit");
            model.AddModelObject(fact.CreateAIOpponent("CarpoRefit.json", model, Quaterniond.Identity, ModelObjectFactory.randVec()));
            Console.WriteLine("Added CarpoRefit");
            model.AddModelObject(fact.CreateAIOpponent("CarpoRefit.json", model, Quaterniond.Identity, ModelObjectFactory.randVec()));
            Console.WriteLine("Added CarpoRefit");
            model.AddModelObject(fact.CreateAIOpponent("CarpoRefit.json", model, Quaterniond.Identity, ModelObjectFactory.randVec()));
            Console.WriteLine("Added CarpoRefit");
            model.AddModelObject(fact.CreateAIOpponent("CarpoRefit.json", model, Quaterniond.Identity, ModelObjectFactory.randVec()));
            Console.WriteLine("Added CarpoRefit");
            ControllableObject obj = fact.CreateControlledObject("MSFV3.json", model, Quaterniond.Identity, ModelObjectFactory.randVec());
            model.AddModelObject(obj);
            View view = new View(0,model, obj, (int)HEIGHT, (int)WIDTH, new Frustum(Angle.CreateDegree(FOVY),(double)WIDTH / HEIGHT,NEAR,FAR), TITLE);
            Console.WriteLine("Loading done.");
            view.Run(UPS, RPS);
            Console.WriteLine("Everything done.");
        }
    }
}
