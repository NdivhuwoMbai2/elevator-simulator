using elevator_simulator.common.Enums;
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
        /// Loads and unload passengers by adding them/subtracting them form the list of people that are in the elevator already
        /// </summary>
        /// <param name="boarding"></param>
        /// <param name="elevator"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Elevator> Boarding(Boarding boarding,Elevator elevator, Request request);
    }
}
