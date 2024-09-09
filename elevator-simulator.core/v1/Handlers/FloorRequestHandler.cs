using elevator_simulator.common.Enums;
using elevator_simulator.common.v1.Interfaces;
using elevator_simulator.common.v1.Models;
using MediatR;

namespace elevator_simulator.core.v1.Handlers
{

    public class FloorRequestHandler : IFloorRequestHandler
    {
        public FloorRequestHandler()
        {

        }
        public async Task<Elevator> Ascend(Elevator elevator, int ElevatorRequest)
        {
            for (int i = elevator.currentFloor; i <= ElevatorRequest; i++)
            {
                elevator.currentFloor = i;
                if (i == ElevatorRequest)
                {
                    await StayIdle(elevator);
                }
                else
                {
                    elevator.Movement = Movement.Motion;
                    elevator.Direction = Direction.Up;
                    PrintElevatorStatus(i, elevator.Movement, elevator.Direction);
                }
            }
            return elevator;
        }

        public async Task<Elevator> Descend(Elevator elevator, int ElevatorRequest)
        {
            if (ElevatorRequest>elevator.currentFloor)
            {
                elevator.ErrorMessage = "Elevator cannot descend to a higher floor";
            }
            
            for (int i = elevator.currentFloor; i >= ElevatorRequest; i--)
            {
                elevator.currentFloor = i;
                if (i == ElevatorRequest)
                {
                    return await StayIdle(elevator);
                }
                else
                {
                    elevator.Movement = Movement.Motion;
                    elevator.Direction = Direction.Down;
                    PrintElevatorStatus(i, elevator.Movement, elevator.Direction);
                }
            }
            return elevator;
        }
        public async Task<Elevator> StayIdle(Elevator elevator)
        {
            elevator.Movement = Movement.Stationary;
            elevator.Direction = Direction.Idle;
            Console.WriteLine($"...Waiting on {elevator.currentFloor}");
            Console.WriteLine();

            return elevator;
        }
        public void PrintElevatorStatus(int i, Movement movement, Direction direction)
        {
            Console.Beep();
            Console.WriteLine($"..going up to floor number : {i}");
            Console.WriteLine($"Elevator is currently in {movement.ToString()} going {direction.ToString()}");
            Console.WriteLine();
        }
        public Elevator GetClosestElevator(int requestedFloor, List<Elevator>? elevators)
        {
            return elevators?.Aggregate((x, y) => Math.Abs(x.currentFloor - requestedFloor) < Math.Abs(y.currentFloor - requestedFloor) ? x : y);
        }
    }
}

