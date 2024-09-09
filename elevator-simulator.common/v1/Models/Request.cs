using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elevator_simulator.common.v1.Models
{
    public class Request
    {
        public int CurrentFloor { get; set; }
        public int Destination { get; set; }
        public int NumberOfPassengers {  get; set; }
    }
}
