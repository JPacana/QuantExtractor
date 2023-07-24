using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantExtractor
{
    internal class Sample
    {
        public string Id = "";

        public readonly Dictionary<string, float> RetentionTimes = new Dictionary<string, float>();

        public readonly Dictionary<string, int> Responses = new Dictionary<string, int>();
    }
}
