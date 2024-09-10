using elevator_simulator.common.v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace elevator_simulator.common.v1.Interfaces
{
    public interface IElevatorRepository
    {
        public List<Elevator>? AddElevator(Elevator elevator, List<Elevator>? elevators);
        public List<string> AddElevatorType(List<string> elevatorTypes,string elevatorType); 
        List<string> LoadElevatorTypes();

        Elevator GetClosestElevator(int requestedFloor, List<Elevator>? elevators);
        Task<List<Elevator>> GetAvailableElevator(Request request, List<Elevator> elevators);
    }
}
