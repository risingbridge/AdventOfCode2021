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
        public Vector Position { get; set; }
        public Dictionary<string, int> Neighbours { get; set; } = new Dictionary<string, int>();
        public int Cost { get; set; }
        public Vector? CostFrom { get; set; }
    }
}
