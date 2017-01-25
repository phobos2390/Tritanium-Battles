using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjLoader.Loader.Loaders;
using OpenTK;
using Newtonsoft.Json.Linq;

namespace LightGameEngine.Model.Creational.Directors
{
    class JSonShipDirector : AbstractShipDirector
    {
        private string mesh;
        private double mass;
        private double thrust;
        private double fuel;
        private double scale;
        private JObject complement;
        private Alignment alignment;
        private JSonComplementDirector missileDirector;

        private Model model;
        private MeshLoader loader;

        public JSonShipDirector(Alignment alignment, double fuel, double thrust, double mass, double scale, JObject complement, Model model, Vector3d position, Quaterniond orientation, MeshLoader loader, string mesh) 
            : base(alignment, fuel, thrust, mass, scale, model, position, orientation, loader.LoadMesh(mesh))
        {
            this.model = model;
            this.loader = loader;
            this.complement = complement;
            this.mesh = mesh;
            this.mass = mass;
            this.thrust = thrust;
            this.fuel = fuel;
            this.scale = scale;
            this.alignment = alignment;
        }

        public static JSonShipDirector fromJSon(JObject json, Model model, Vector3d position, Quaterniond orientation, MeshLoader loader)
        {
            string mesh = json["mesh"].CreateReader().ReadAsString();
            double mass = json["mass"].CreateReader().ReadAsDouble().Value;
            double thrust = json["thrust"].CreateReader().ReadAsDouble().Value;
            double fuel = json["fuel"].CreateReader().ReadAsDouble().Value;
            double scale = json["scale"].CreateReader().ReadAsDouble().Value;
            JObject complement = (JObject) json["complement"];
            Alignment alignment;
            string alignmentString = json["alignment"].CreateReader().ReadAsString();
            if(alignmentString.ToUpper() == Alignment.Jovian.ToString().ToUpper())
            {
                alignment = Alignment.Jovian;
            }
            else if(alignmentString.ToUpper() == Alignment.Martian.ToString().ToUpper())
            {
                alignment = Alignment.Martian;
            }
            else
            {
                alignment = Alignment.Neutral;
            }
            return new JSonShipDirector(alignment, fuel, thrust, mass, scale, complement, model, position, orientation, loader, mesh);
        }

        public ShipObject CreateShip(ShipObjectBuilder shipBuilder, ModelObjectBuilder modelObjectBuilder)
        {
            IModelObject core = modelObjectBuilder.CreateModelObject();
            return CreateShip(shipBuilder, core, JSonComplementDirector.CreateDirector(complement, core, thrust, model, loader));
        }

        public override ShipObject CreateShip()
        {
            ModelObjectBuilder modelBuilder = new ModelObjectBuilder();
            IModelObject core = createCoreObject(modelBuilder);
            return CreateShip(new ShipObjectBuilder(), core, JSonComplementDirector.CreateDirector(complement, core, thrust, model, loader));
        }
    }
}
