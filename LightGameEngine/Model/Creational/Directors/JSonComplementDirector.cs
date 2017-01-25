using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using OpenTK;

namespace LightGameEngine.Model.Creational.Directors
{
    class JSonComplementDirector : AbstractComplementDirector
    {
        List<MissileSpecification> specifications;
        List<MissileHardPoint> hardPoints;

        public JSonComplementDirector(List<MissileSpecification> specs, List<MissileHardPoint> hardPts, IModelObject firedBy, double thrust, Model model, MeshLoader loader) 
            : base(firedBy, thrust, model, loader)
        {
            specifications = specs;
            hardPoints = hardPts;
        }

        public static IComplementDirector CreateDirector(JObject complement, IModelObject firedBy, double thrust, Model model, MeshLoader loader)
        {
            List<MissileSpecification> specs = new List<MissileSpecification>();
            JArray missileArray = (JArray)complement["missiles"];
            foreach(JObject missileSpec in missileArray)
            {
                int hardPoint = missileSpec["hardpoint"].CreateReader().ReadAsInt32().Value;
                string type = missileSpec["type"].CreateReader().ReadAsString();
                int count = missileSpec["number"].CreateReader().ReadAsInt32().Value;
                specs.Add(new MissileSpecification(hardPoint, type, count));
            }
            List<MissileHardPoint> hardPoints = new List<MissileHardPoint>();
            JArray hardPointsArray = (JArray)complement["hardpoints"];
            foreach(JObject hardPointObject in hardPointsArray)
            {
                double x = hardPointObject["x"].CreateReader().ReadAsDouble().Value;
                double y = hardPointObject["y"].CreateReader().ReadAsDouble().Value;
                double z = hardPointObject["z"].CreateReader().ReadAsDouble().Value;
                hardPoints.Add(new MissileHardPoint(new Vector3d(x, y, z)));
            }
            return new JSonComplementDirector(specs, hardPoints, firedBy, thrust, model, loader);
        }

        public override IList<MissileArray> CreateComplement()
        {
            return CreateComplement(new MissileArrayBuilder());
        }

        public override IList<MissileArray> CreateComplement(MissileArrayBuilder builder)
        {
            return CreateComplement(builder, specifications, hardPoints);
        }
    }
}