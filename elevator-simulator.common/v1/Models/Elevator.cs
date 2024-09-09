using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elevator_simulator.common.v1.Models
{
    public class Elevator : Status
    {
        public string Name { get; set; }
        public int TopFloor { get; set; }
        public int MaximumCapacity { get; set; }
        public string ElevatorType { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
