using elevator_simulator.common.v1.Interfaces;
using elevator_simulator.common.v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elevator_simulator.core.v1.Handlers
{
    public class PassengerHandler : IPassengerHandler
    {
        public async Task<Elevator> DropPassengers(Elevator elevator, Request request)
        {
            if (elevator.Movement == common.Enums.Movement.Stationary)
            {
                elevator.PassengerCount = elevator.PassengerCount - request.NumberOfPassengers;

                // elevator.PassengerCount = Math.Max(0, elevator.PassengerCount);
                Console.WriteLine($"Drooped {request.NumberOfPassengers} number of passengers");
                Console.WriteLine($"Elevator {elevator.Name} has {elevator.PassengerCount} passengers");
            }
            return await Task.FromResult(elevator);
        }
        public async Task<Elevator> PickUpPassengers(Elevator elevator, Request request)
        {
            if (elevator.Movement == common.Enums.Movement.Stationary)
            {
                elevator.PassengerCount = elevator.PassengerCount + request.NumberOfPassengers;
                Console.WriteLine($"Add {request.NumberOfPassengers} number of passengers");
                Console.WriteLine($"Elevator {elevator.Name} has {elevator.PassengerCount} passengers");
            }
            return await Task.FromResult(elevator);
        }
    }
}
