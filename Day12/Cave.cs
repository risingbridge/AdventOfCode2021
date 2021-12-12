using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    internal class Cave
    {
        public Cave(string name, bool isBig, List<string> pathTo)
        {
            Name = name;
            this.isBig = isBig;
            PathTo = pathTo;
        }
        public string Name { get; set; }
        public bool isBig { get; set; }
        public List<string> PathTo { get; set; }
    }
}
