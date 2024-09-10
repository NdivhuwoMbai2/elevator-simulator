using elevator_simulator.common.v1.Interfaces;
using elevator_simulator.common.v1.Models;

namespace elevator_simulator.core.v1.Handlers
{
    public class QueueHandler : IQueueHandler
    {
        public bool? isSystemRunning = false;
        public IFloorRequestHandler floorRequestHandler;
        public QueueHandler(IFloorRequestHandler floorRequestHandler)
        {
            this.floorRequestHandler = floorRequestHandler;
        }
        public async Task<Queue<Request>> Add(Request request, Queue<Request> ElevatorQueue)
        {
            if (!ElevatorQueue.Any(e => e.CurrentFloor == request.CurrentFloor))
            {
                //add to a queue
                ElevatorQueue.Enqueue(request);
            }
            else
            {
                Console.WriteLine("elevator on its way");
            }
            return await Task.FromResult(ElevatorQueue);
        }

        public async Task<Elevator> SendElevatorToDropOff(Request request, Elevator elevator)
        {
            if (elevator.currentFloor > request.Destination)
            {
                // the elevator must go down
                Console.WriteLine("Going down");
                elevator = await floorRequestHandler.Descend(elevator, request.Destination);
            }
            else
            {
                Console.WriteLine("Going up");
                elevator = await floorRequestHandler.Ascend(elevator, request.Destination);
                // the elevator must go up
            }
            return elevator;
        }


        public async Task<Elevator> SendElevatorToPickup(Request request, Elevator elevator)
        {
            if (request.CurrentFloor < elevator.currentFloor)
            {
                // the elevator must go down
                elevator = await floorRequestHandler.Descend(elevator, request.CurrentFloor);
            }
            else
            {
                // the elevator must go up
                elevator = await floorRequestHandler.Ascend(elevator, request.CurrentFloor);
            }
            return elevator;
        } 
    }
}
