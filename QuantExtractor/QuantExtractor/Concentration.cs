using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantExtractor
{
    internal class Concentration
    {
        public readonly string InternalStandard = "Morphine-D3";

        public readonly string[] Analytes = new string[] { "Morphine", "Codeine", "Thebaine" };

        public readonly List<Sample> Samples = new List<Sample>();
    }
}
