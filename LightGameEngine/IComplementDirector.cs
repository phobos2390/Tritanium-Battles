using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public interface IComplementDirector
    {
        IList<MissileArray> CreateComplement(MissileArrayBuilder builders);
        IList<MissileArray> CreateComplement();
    }
}
