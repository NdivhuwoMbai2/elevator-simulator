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
            //arrange
            var ls = new Queue<Request>();

            //act
            var result = iqueueHandler.Add(fixture.request, ls).Result;

            //assert
            Assert.True(result.Count > 0);
        }
        [Fact]
        public void adding_duplicate_invalid()
        {
            //arrange
            var ls = new Queue<Request>();
            ls.Enqueue(new Request() { CurrentFloor = 1, Destination = 2 });

            //act
            var result = iqueueHandler.Add(new Request() { CurrentFloor = 1, Destination = 2 }, ls).Result;

            //assert
            Assert.False(result.Count > 1);
        }
        [Fact]
        public void SendElevatorToDropOff_valid()
        {
            //arrange
            var ls = new Queue<Request>();

            //act
            var result = iqueueHandler.SendElevatorToDropOff(fixture.request, fixture.elevator).Result;

            //assert
            Assert.True(result.Direction == common.Enums.Direction.Idle);
        }
        [Fact]
        public void SendElevatorToDropOff_go_down_valid()
        {
            //arrange
            var ls = new Queue<Request>();
            var elevator = fixture.elevator;
            elevator.currentFloor = 2;

            //act
            var result = iqueueHandler.SendElevatorToDropOff(fixture.request, elevator).Result;

            //assert
            Assert.True(result.Direction == common.Enums.Direction.Idle);
        }
        [Fact]
        public void SendElevatorToPickup_valid()
        {
            //arrange
            var ls = new Queue<Request>();

            //act
            var result = iqueueHandler.SendElevatorToPickup(fixture.request, fixture.elevator).Result;

            //assert
            Assert.True(result.Direction == common.Enums.Direction.Idle);
        }
        [Fact]
        public void SendElevatorToPickup_go_up_valid()
        {
            //arrange
            var ls = new Queue<Request>();

            var req = fixture.request;
            req.CurrentFloor = 2; 

            //act
            var result = iqueueHandler.SendElevatorToPickup(req, fixture.elevator).Result;

            //assert
            Assert.True(result.Direction == common.Enums.Direction.Idle);
        }
    }
}
