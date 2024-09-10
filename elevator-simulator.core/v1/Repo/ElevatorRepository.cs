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
            ElevatorType elevatorType = new();
            elevatorType.ElevatorTypes.Add("high-speed");
            elevatorType.ElevatorTypes.Add("glass");
            elevatorType.ElevatorTypes.Add("freight");
            return elevatorType.ElevatorTypes;
        }
        public Elevator GetClosestElevator(int requestedFloor, List<Elevator>? elevators)
        {
            return elevators?.Aggregate((x, y) => Math.Abs(x.currentFloor - requestedFloor) < Math.Abs(y.currentFloor - requestedFloor) ? x : y);
        }
        public async Task<List<Elevator>> GetAvailableElevator(Request request, List<Elevator> elevators)
        {
            return elevators.Where(e => e.MaximumCapacity < (request.NumberOfPassengers + e.PassengerCount)).ToList();
        }
    }
}
