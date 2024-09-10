using elevator_simulator.common.v1.Models;

namespace elevator_simulator.common.v1.Interfaces
{
    public interface IFloorRequestHandler
    {
        /// <summary>
        /// Sends an elevator to up to the floors above
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="ElevatorRequest"></param>
        /// <returns>returns the state of the elevator and the new floor</returns>
        public Task<Elevator> Ascend(Elevator elevator, int ElevatorRequest);
        /// <summary>
        /// sends elevator to the lower floors in the building
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="ElevatorRequest"></param>
        /// <returns>returns the state of the elevator and the new floor</returns>
        public Task<Elevator> Descend(Elevator elevator, int ElevatorRequest);
        /// <summary>
        /// Sets the state of the elevator to be stationary and be Idle
        /// </summary>
        /// <param name="elevator"></param>
        /// <returns>returns the state of the elevator and the new floor</returns>
        public Task<Elevator> StayIdle(Elevator elevator); 
    }
}
