using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    internal class Vector
    {
        public Vector(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool Compare(Vector a, Vector b)
        {
            if(a.x == b.x && a.y == b.y)
            {
                return true;
            }
            return false;
        }

        public static bool ListContain(List<Vector>list, Vector a)
        {
            foreach(Vector v in list)
            {
                if(v.x == a.x && v.y == a.y)
                {
                    return true;
                }
            }
            return false;
        }

        public int x { get; set; }
        public int y { get; set; }
    }
}
