using elevator_simulator.common.v1.Interfaces;
using elevator_simulator.common.v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elevator_simulator.core.v1.Handlers
{
    public class ElevatorHandler : IElevatorHandler
    {

        public List<Elevator>? Add(Elevator? elevator, List<Elevator>? elevators)
        {
            if (elevator != null)
            {
                elevators?.Add(elevator);
            }
            return elevators;
        }

        public List<string> AddElevatorType(List<string> elevatorTypes, string elevatorType)
        {
            elevatorTypes.Add(elevatorType);
            return elevatorTypes;
        }

        public List<string> LoadElevatorTypes() => new List<string>() { "high-speed", "glass", "freight" };

        public async Task<Elevator> GetClosestElevator(int requestedFloor, List<Elevator>? elevators)
        {
            return await Task.FromResult(elevators?.Aggregate((x, y) => Math.Abs(x.currentFloor - requestedFloor) < Math.Abs(y.currentFloor - requestedFloor) ? x : y));
        }

        public async Task<List<Elevator>> GetElevatorWithSpace(Request request, List<Elevator> elevators) 
            => await Task.FromResult(elevators.Where(e => e.MaximumCapacity > request.NumberOfPassengers + e.PassengerCount).ToList());
    }
}
