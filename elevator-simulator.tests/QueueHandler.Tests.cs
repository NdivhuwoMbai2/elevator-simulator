using elevator_simulator.common.v1.Interfaces;
using elevator_simulator.common.v1.Models;
using elevator_simulator.tests.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elevator_simulator.tests
{
    public class QueueHandler_Tests : IClassFixture<TestDataFixture>
    {
        public IQueueHandler iqueueHandler;
        public TestDataFixture fixture;
        public QueueHandler_Tests(IQueueHandler iqueueHandler, TestDataFixture Fixture)
        {
            this.iqueueHandler = iqueueHandler;
            fixture = Fixture;
        }

        [Fact]
        public void AddItemsTo_Queue_valid()
        {
            var ls = new Queue<Request>();
            var result = iqueueHandler.Add(fixture.request, ls).Result;

            Assert.True(result.Count > 0);
        }
        [Fact]
        public void adding_duplicate_invalid()
        {
            var ls = new Queue<Request>();
            ls.Enqueue(new Request() { CurrentFloor = 1, Destination = 2 });
            var result = iqueueHandler.Add(new Request() { CurrentFloor = 1, Destination = 2 }, ls).Result;

            Assert.False(result.Count > 1);
        }
        [Fact]
        public void SendElevatorToDropOff_valid()
        {
            var ls = new Queue<Request>();
            var result = iqueueHandler.SendElevatorToDropOff(fixture.request, fixture.elevator).Result;

            Assert.True(result.Direction == common.Enums.Direction.Idle);
        }
        [Fact]
        public void SendElevatorToDropOff_go_down_valid()
        {
            var ls = new Queue<Request>();
            var elevator = fixture.elevator;
            elevator.currentFloor = 2;
            var result = iqueueHandler.SendElevatorToDropOff(fixture.request, elevator).Result;

            Assert.True(result.Direction == common.Enums.Direction.Idle);
        }
        [Fact]
        public void SendElevatorToPickup_valid()
        {
            var ls = new Queue<Request>();
            var result = iqueueHandler.SendElevatorToPickup(fixture.request, fixture.elevator).Result;

            Assert.True(result.Direction == common.Enums.Direction.Idle);
        }
        [Fact]
        public void SendElevatorToPickup_go_up_valid()
        {
            var ls = new Queue<Request>();

            var req = fixture.request;
            req.CurrentFloor = 2; 
            var result = iqueueHandler.SendElevatorToPickup(req, fixture.elevator).Result;

            Assert.True(result.Direction == common.Enums.Direction.Idle);
        }
    }
}
