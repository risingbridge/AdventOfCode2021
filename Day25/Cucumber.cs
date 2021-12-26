using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day25
{
    internal class Cucumber
    {
        public Cucumber(Vector pos, CucumberType type, bool canMove)
        {
            Pos = pos;
            Type = type;
            CanMove = canMove;
        }

        public override string ToString()
        {
            return $"Position: {Pos.ToString()}, Type: {Type.ToString()}, Can move: {CanMove}";
        }

        public Guid ID { get; set; } = Guid.NewGuid();

        public Vector Pos { get; set; }
        public CucumberType Type { get; set; }
        public bool CanMove { get; set; } = false;

    }

    internal enum CucumberType
    {
        East,
        South
    }
}
