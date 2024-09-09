﻿using elevator_simulator.common.v1.Interfaces;
using elevator_simulator.common.v1.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<Queue<Request>> AddToQueue(Request reqest,Queue<Request> ElevatorQueue)
        {
            if (!ElevatorQueue.Any(e => e.CurrentFloor == reqest.CurrentFloor))
            {
                //add to a queue
                ElevatorQueue.Enqueue(reqest);
            }
            else
            {
                Console.WriteLine("elevator on its way");
            } 
            return ElevatorQueue;
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
            } return elevator;
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
            } return elevator;
        }
        public async Task<Elevator> DropPassengers(Elevator elevator, Request request)
        {
            if (elevator.Movement == common.Enums.Movement.Stationary)
            {
                elevator.PassengerCount = -request.NumberOfPassengers;

                elevator.PassengerCount = Math.Max(0, elevator.PassengerCount);
                Console.WriteLine($"Drooped {request.NumberOfPassengers} number of passengers");
                Console.WriteLine($"Elevator {elevator.Name} has {elevator.PassengerCount} passengers");
            }
            return elevator;
        }
        public async Task<Elevator> PickUpPassengers(Elevator elevator, Request request)
        {
            if (elevator.Movement == common.Enums.Movement.Stationary)
            {
                elevator.PassengerCount = +request.NumberOfPassengers;
                Console.WriteLine($"Add {request.NumberOfPassengers} number of passengers");
                Console.WriteLine($"Elevator {elevator.Name} has {elevator.PassengerCount} passengers");
            }
            return elevator;
        }
      
    }
}
