using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day24
{
    internal class Instruction
    {
        public Instruction(string? opCode, string? modA, string? modB)
        {
            OpCode = opCode;
            ModA = modA;
            ModB = modB;
        }

        public string? OpCode { get; set; }
        public string? ModA { get; set; }
        public string? ModB { get; set; }

    }
}
