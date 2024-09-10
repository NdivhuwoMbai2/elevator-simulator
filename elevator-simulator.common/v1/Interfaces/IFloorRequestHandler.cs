using elevator_simulator.common.v1.Models;

namespace elevator_simulator.common.v1.Interfaces
{
    public interface IFloorRequestHandler
    {
        public Task<Elevator> Ascend(Elevator elevator, int ElevatorRequest);
        public Task<Elevator> Descend(Elevator elevator, int ElevatorRequest);
        public Task<Elevator> StayIdle(Elevator elevator); 
    }
}
