using elevator_simulator.common.v1.Interfaces;
using elevator_simulator.common.v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elevator_simulator.core.v1.Repo
{
    public class ElevatorRepository : IElevatorRepository
    {

        public List<Elevator>? AddElevator(Elevator elevator, List<Elevator>? elevators)
        {
            elevators?.Add(elevator);
            return elevators;
        }

        public List<string> AddElevatorType(List<string> elevatorTypes, string elevatorType)
        {
            elevatorTypes.Add(elevatorType);
            return elevatorTypes;
        }
 
        public List<string> LoadElevatorTypes()
        {
            ElevatorType elevatorType = new ElevatorType();
            elevatorType.ElevatorTypes.Add("high-speed");
            elevatorType.ElevatorTypes.Add("glass");
            elevatorType.ElevatorTypes.Add("freight");
            return elevatorType.ElevatorTypes;
        }
    }
}
