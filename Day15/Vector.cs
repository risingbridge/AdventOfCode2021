using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    internal class Vector
    {
        public Vector(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Vector p = (Vector)obj;
                Console.WriteLine($"testing");
                return (x == p.x) && (y == p.y);
            }

        }

        public static bool operator ==(Vector a, Vector b)
        {
            if (System.Object.ReferenceEquals(a, b))
                return true;

            if ((object)a == null || (object)b == null)
                return false;

            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Vector a, Vector b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"{x},{y}";
        }


        public int x { get; set; }
        public int y { get; set; }
    }
}
