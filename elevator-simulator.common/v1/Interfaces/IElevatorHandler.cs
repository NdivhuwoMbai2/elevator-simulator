using elevator_simulator.common.v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace elevator_simulator.common.v1.Interfaces
{
    public interface IElevatorHandler
    {
        /// <summary>
        /// Add an elevator to a collection of elevators
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="elevators"></param>
        /// <returns>returns a list of elevators including the newly added one</returns>
        public List<Elevator>? Add(Elevator elevator, List<Elevator>? elevators);
        /// <summary>
        /// Adds a new elevator type
        /// </summary>
        /// <param name="elevatorTypes"></param>
        /// <param name="elevatorType"></param>
        /// <returns>returns a list of elevator types including the newly added elevatortype</returns>
        public List<string> AddElevatorType(List<string> elevatorTypes, string elevatorType);
        /// <summary>
        /// Gets list of elevator types
        /// </summary>
        /// <returns>returns a list of default elevator types</returns>
        List<string> LoadElevatorTypes();
        /// <summary>
        /// Retrieves the closest elevator by using the requested floor and the state of the elevators
        /// </summary>
        /// <param name="requestedFloor"></param>
        /// <param name="elevators"></param>
        /// <returns>returns an elevator that is closest to the requested floor</returns>
        Task<Elevator> GetClosestElevator(int requestedFloor, List<Elevator>? elevators);
        /// <summary>
        /// retrieves the elevator with an available space
        /// </summary>
        /// <param name="request"></param>
        /// <param name="elevators"></param>
        /// <returns>returns all elevators with space for an extra passenger</returns>
        Task<List<Elevator>> GetElevatorWithSpace(Request request, List<Elevator> elevators);
    }
}
