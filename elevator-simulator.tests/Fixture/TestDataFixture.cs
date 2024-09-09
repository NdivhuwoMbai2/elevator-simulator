using elevator_simulator.common.v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elevator_simulator.tests.Fixture
{
    public class TestDataFixture : IDisposable
    {
        public Elevator elevator;
        public List<Elevator> Elevators;
        public Request request;
        public TestDataFixture()
        {

            Elevators = new List<Elevator>() {
                new Elevator { Name = "test1", currentFloor = 10 },
                new Elevator { Name = "test2", currentFloor = 5 } };
            elevator = new Elevator()
            {
                Name = "test1",
                currentFloor = 0,
            };
            request = new Request() { CurrentFloor = 5 };
        }

        public void Dispose()
        {
            elevator = null;
            Elevators.Clear();
            request = null; 
        }
    }
}
