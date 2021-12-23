using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9
{
    internal class Vector
    {
        public int x { get; set; }
        public int y { get; set; }

        public string ToString()
        {
            return $"X:{x} Y:{y}";
        }
    }
}
