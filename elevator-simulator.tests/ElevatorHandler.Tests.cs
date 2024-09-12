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

    public class ElevatorHandler_tests : IClassFixture<TestDataFixture>
    {
        public IElevatorHandler ElevatorHandler;
        public TestDataFixture Fixture;
        public ElevatorHandler_tests(IFloorRequestHandler FloorRequestHandler, IElevatorHandler ElevatorHandler, TestDataFixture Fixture)
        {
            this.Fixture = Fixture;
            this.ElevatorHandler = ElevatorHandler;
        }
        [Fact]
        public void Add_elevator_valid()
        {
            //act
            var result = ElevatorHandler.Add(Fixture.elevator, Fixture.Elevators);

            //assert
            Assert.True(result.Count > 0);
        }
        [Fact]
        public void Add_elevator_inValid()
        {
            //act
            var result = ElevatorHandler.Add(null, new List<common.v1.Models.Elevator>() { });

            //assert
            Assert.False(result.Count > 0);
        }
        [Fact]
        public void Get_elevator_with_space_valid()
        {
            //act
            var actualResult = ElevatorHandler.GetElevatorWithSpace(Fixture.request, Fixture.Elevators).Result;

            //assert
            Assert.NotNull(actualResult);
        }
        [Theory]
        [InlineData(11, 10)]

        public void Get_elevator_with_space_invalid(int numberOfPassengers, int maximumCapacity)
        {
            //arrange
            int expected = 0;
            Request request = new() { NumberOfPassengers = numberOfPassengers };
            List<Elevator> elevators = [new() { MaximumCapacity = maximumCapacity }];

            //act
            var actualElevator = ElevatorHandler.GetElevatorWithSpace(request, elevators).Result;

            //assert
            Assert.True(actualElevator.Count == 0);
        }
        [Theory]
        [InlineData(2)]
        public void Get_nearest_elevator_valid(int requestingFloor)
        {
            //arrange 
            int expected = 0;

            //act
            var actualElevator = ElevatorHandler.GetClosestElevator(requestingFloor, Fixture.Elevators).Result;

            //assert
            Assert.Equal(expected, actualElevator.currentFloor);
        }
        [Theory]
        [InlineData(8)]
        public void Get_nearest_elevator_invalid(int requestingFloor)
        {
            //arrange
            int expected = 5;

            //act
            var actual = ElevatorHandler.GetClosestElevator(requestingFloor, Fixture.Elevators).Result;

            //assert
            Assert.NotEqual(expected, actual.currentFloor);
        }
        [Theory]
        [InlineData("Test")]
        public void Add_elevator_types_valid(string elevatorType)
        {
            //arrange
            string expected = "Test";
            var elevatorTypes = ElevatorHandler.LoadElevatorTypes();

            //act
            var result = ElevatorHandler.AddElevatorType(elevatorTypes, elevatorType);

            var actual = result.FirstOrDefault(e => e == "Test");

            //assert
            Assert.Equal(expected, actual);
        }
        [Theory]
        [InlineData("glasS")]
        public void Add_an_already_existing_elevator_types_invalid_throws_ArgumentException(string elevatorType)
        {
            //arrange 
            var elevatorTypes = ElevatorHandler.LoadElevatorTypes(); 
            // act & assert 
            Assert.Throws<ArgumentException>(() => ElevatorHandler.AddElevatorType(elevatorTypes, elevatorType));
        }
    }
}
