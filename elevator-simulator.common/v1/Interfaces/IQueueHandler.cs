using elevator_simulator.common.v1.Models;

namespace elevator_simulator.common.v1.Interfaces
{
    public interface IQueueHandler
    { 
        /// <summary>
        /// Adds a new request to a queue
        /// </summary>
        /// <param name="reqest"></param>
        /// <param name="ElevatorQueue"></param>
        /// <returns>returns a list of quues including the new one</returns>
        Task<Queue<Request>> Add(Request reqest, Queue<Request> ElevatorQueue);
        /// <summary>
        /// navagates elevator to a new floor to dropoff
        /// </summary>
        /// <param name="request"></param>
        /// <param name="elevator"></param>
        /// <returns>returns an elevator with the new state and the new currentfloor and Idle</returns>
        Task<Elevator> SendElevatorToDropOff(Request request, Elevator elevator);
        /// <summary>
        /// Sends elevator to a floor that was requested
        /// </summary>
        /// <param name="request"></param>
        /// <param name="elevator"></param>
        /// <returns>returns an elevator with the new state and the new currentfloor and Idle</returns>
        Task<Elevator> SendElevatorToPickup(Request request, Elevator elevator);
    }
}
