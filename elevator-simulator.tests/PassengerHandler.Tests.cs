﻿using elevator_simulator.common.v1.Interfaces;
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
        public PassengerHander_test(IPassengerHandler PassengerHandler, TestDataFixture Fixture)
        {
            this.fixture = Fixture;
            this.PassengerHandler = PassengerHandler;
        }
        [Theory]
        [InlineData(2, 4)]
        public void DropOff_passengers_valid(int numberOfPassengers, int passengerCount)
        {
            //arrange
            int expected = 2;
            var request = fixture.request;
            request.NumberOfPassengers = numberOfPassengers;

            var elevator = fixture.elevator;
            elevator.PassengerCount = passengerCount;
            elevator.Movement = common.Enums.Movement.Stationary;

            //act
            var result = PassengerHandler.Boarding(common.Enums.Boarding.Out, fixture.elevator, request).Result;

            //assert
            Assert.Equal(expected, result.PassengerCount);
        }
        [Theory]
        [InlineData(2, 4)]
        public void DropOff_passengers_Invalid(int numberOfPassengers, int passengerCount)
        {
            //arrange
            int expected = 2;
            var request = fixture.request;
            request.NumberOfPassengers = numberOfPassengers;

            var elevator = fixture.elevator;
            elevator.PassengerCount = passengerCount;
            elevator.Movement = common.Enums.Movement.Motion;

            //act
            var actual = PassengerHandler.Boarding(common.Enums.Boarding.Out, fixture.elevator, request).Result;

            //assert
            Assert.NotEqual(expected, actual.PassengerCount);
        }
        [Theory]
        [InlineData(2, 4)]
        public void PickUp_passengers_valid(int numberOfPassengers, int passengerCount)
        {
            //arrange
            int expected = 6;
            var request = fixture.request;
            request.NumberOfPassengers = numberOfPassengers;

            var elevator = fixture.elevator;
            elevator.PassengerCount = passengerCount;
            elevator.Movement = common.Enums.Movement.Stationary;

            //act
            var result = PassengerHandler.Boarding(common.Enums.Boarding.In, fixture.elevator, request).Result;

            //assert
            Assert.Equal(expected, result.PassengerCount);
        }
        [Theory]
        [InlineData(2, 6)]
        public void PickUp_passengers_invalid(int numberOfPassengers, int passengerCount)
        {
            //arrange
            int expected = 2;
            var request = fixture.request;
            request.NumberOfPassengers = numberOfPassengers;

            var elevator = fixture.elevator;
            elevator.PassengerCount = passengerCount;
            elevator.Movement = common.Enums.Movement.Stationary;

            //act
            var result = PassengerHandler.Boarding(common.Enums.Boarding.Out, fixture.elevator, request).Result;

            //assert
            Assert.NotEqual(expected, result.PassengerCount);
        }
        [Theory]
        [InlineData(2, 6)]
        public void PickUp_passengers_failed(int numberOfPassengers, int passengerCount)
        {
            //arrange
            int expected = 2;
            var request = fixture.request;
            request.NumberOfPassengers = numberOfPassengers;

            var elevator = fixture.elevator;
            elevator.PassengerCount = passengerCount;
            elevator.Movement = common.Enums.Movement.Stationary;

            var boarding = (common.Enums.Boarding)(-1);

            //act && assert
            Assert.Throws<AggregateException>(() => PassengerHandler.Boarding(  boarding, fixture.elevator, request).Result); 
        }
    }
}
