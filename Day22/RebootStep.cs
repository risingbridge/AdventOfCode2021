using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day22
{
    internal class RebootStep
    {
        public RebootStep(Vector3 stepFrom, Vector3 stepTo, bool turnOn)
        {
            StepFrom = stepFrom;
            StepTo = stepTo;
            TurnOn = turnOn;
        }

        public bool TurnOn { get; set; }
        public Vector3 StepFrom { get; set; }
        public Vector3 StepTo { get; set; }

    }
}
