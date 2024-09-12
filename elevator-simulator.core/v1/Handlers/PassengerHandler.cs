using elevator_simulator.common.Enums;
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
        
        public async Task<Elevator> Boarding(Boarding boarding, Elevator elevator, Request request)
        {
            if (elevator.Movement == common.Enums.Movement.Stationary)
            {
                switch (boarding)
                {
                    case common.Enums.Boarding.In:
                        elevator.PassengerCount = elevator.PassengerCount + request.NumberOfPassengers;
                        break;
                    case common.Enums.Boarding.Out:
                        elevator.PassengerCount = elevator.PassengerCount - request.NumberOfPassengers;
                        break;
                    default:
                        Console.Error.WriteLine("no valid boarding state entered");
                        throw new ArgumentException("no valid boarding state entered"); 
                } 
                Console.WriteLine($"{request.NumberOfPassengers} number of passengers boarded {boarding.ToString()}");
                Console.WriteLine($"Elevator {elevator.Name} has {elevator.PassengerCount} passengers");
            }
            return await Task.FromResult(elevator);
        }
    }
}
