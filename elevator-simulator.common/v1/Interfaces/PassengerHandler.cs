using elevator_simulator.common.v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elevator_simulator.common.v1.Interfaces
{
    public interface IPassengerHandler
    {

        Task<Elevator> DropPassengers(Elevator elevator, Request request);
        Task<Elevator> PickUpPassengers(Elevator elevator, Request request);
    }
}
