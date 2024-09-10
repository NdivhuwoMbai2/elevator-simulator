using elevator_simulator.common.v1.Interfaces;
using elevator_simulator.common.v1.Models;
using elevator_simulator.core.v1.Handlers;
using elevator_simulator.tests.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elevator_simulator.tests
{
    public class PassengerHander_test : IClassFixture<TestDataFixture>
    { 
        public IPassengerHandler PassengerHandler;
        public TestDataFixture fixture;
        public PassengerHander_test( IPassengerHandler PassengerHandler, TestDataFixture Fixture)
        { 
            this.fixture = Fixture;
            this.PassengerHandler = PassengerHandler;
        } 
        [Theory]
        [InlineData(2,4)]
        public void DropOff_passengers_valid(int numberOfPassengers,int passengerCount)
        {
            var ls = new Queue<Request>();

            var req = fixture.request;
            req.NumberOfPassengers = numberOfPassengers;


            var ele = fixture.elevator;
            ele.PassengerCount = passengerCount;
            ele.Movement = common.Enums.Movement.Stationary;

            var result = PassengerHandler.DropPassengers(fixture.elevator, req).Result;

            Assert.True(result.PassengerCount == 2);
        }
    }
}
