using elevator_simulator.common.v1.Models;

namespace elevator_simulator.common.v1.Interfaces
{
    public interface IQueueHandler
    { 
        Task<Queue<Request>> Add(Request reqest, Queue<Request> ElevatorQueue);
        Task<Elevator> SendElevatorToDropOff(Request request, Elevator elevator);
        Task<Elevator> SendElevatorToPickup(Request request, Elevator elevator);
    }
}
