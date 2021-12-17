using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day15;

namespace Day15
{
    internal class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Dictionary<Vector, int>? Neighbours { get; set; }

    }
}
