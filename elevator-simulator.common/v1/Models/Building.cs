using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elevator_simulator.common.v1.Models
{ 
    public class Building
    {
        public Building(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public List<Elevator> Elevators { get; set; } = new();
    }
}
