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
        /// <summary>
        /// Drops off passengers from the elevator by subtracting the number of people in the elevator by the number requested
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="request"></param>
        /// <returns>Returns the elevator with less number of people</returns>
        Task<Elevator> DropPassengers(Elevator elevator, Request request);
        /// <summary>
        /// Picks up passesngers by adding them to the list of people that are in the elevator already
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="request"></param>
        /// <returns>returns the elevator with extra number of people</returns>
        Task<Elevator> PickUpPassengers(Elevator elevator, Request request);
    }
}
