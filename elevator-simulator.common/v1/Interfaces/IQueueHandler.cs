using elevator_simulator.common.v1.Models;

namespace elevator_simulator.common.v1.Interfaces
{
    public interface IQueueHandler
    { 
        Task<Queue<Request>> AddToQueue(Request reqest, Queue<Request> ElevatorQueue);
        Task<Elevator> SendElevatorToDropOff(Request request, Elevator elevator);
        Task<Elevator> SendElevatorToPickup(Request request, Elevator elevator);
        Task<Elevator> DropPassengers(Elevator elevator, Request request);
        Task<Elevator> PickUpPassengers(Elevator elevator, Request request);
    }
}
