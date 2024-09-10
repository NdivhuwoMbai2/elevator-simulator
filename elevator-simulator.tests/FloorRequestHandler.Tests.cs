using elevator_simulator.common.v1.Interfaces;
using elevator_simulator.common.v1.Models;
using elevator_simulator.core.v1.Handlers;
using elevator_simulator.tests.Fixture;
using Microsoft.Extensions.DependencyInjection;

namespace elevator_simulator.tests
{

    public class FloorRequestHandler_Tests : IClassFixture<TestDataFixture>
    {

        public IFloorRequestHandler FloorRequestHandler;
        public IElevatorHandler ElevatorRepository;
        public TestDataFixture Fixture;
        public FloorRequestHandler_Tests(IFloorRequestHandler FloorRequestHandler, IElevatorHandler ElevatorRepository, TestDataFixture Fixture)
        {
            this.FloorRequestHandler = FloorRequestHandler;
            this.Fixture = Fixture;
            this.ElevatorRepository = ElevatorRepository;
        }
        [Fact]
        public void Decend_input_2floor_returns_invalidvalid()
        {
            Fixture.elevator.currentFloor = 2;
            var result = FloorRequestHandler.Descend(Fixture.elevator, 5).Result;

            Assert.False(result.currentFloor == 3);
            Assert.NotEmpty(result.ErrorMessage);
        }
        [Fact]
        public void Decend_input_2floor_returns_valid()
        {
            Fixture.elevator.currentFloor = 5;
            var result = FloorRequestHandler.Descend(Fixture.elevator, 2).Result;

            Assert.True(result.currentFloor == 2);
            Assert.Empty(result.ErrorMessage);
        }
        [Fact]
        public void Ascend_input_2floor_returns_valid()
        {
            Fixture.elevator.currentFloor = 0;
            var result = FloorRequestHandler.Ascend(Fixture.elevator, 2).Result;

            Assert.True(result.currentFloor == 2);
            Assert.Empty(result.ErrorMessage);
        }

        [Fact]
        public void Get_nearest_elevator_valid()
        {
            var result = ElevatorRepository.GetClosestElevator(2, Fixture.Elevators);

            Assert.True(result.currentFloor == 5);
        }
        [Fact]
        public void Get_nearest_elevator_invalid()
        {
            var result = ElevatorRepository.GetClosestElevator(2, Fixture.Elevators);

            Assert.False(result.currentFloor == 10);
        }
        [Fact]
        public void SetIdleState_valid_invalid()
        {
            var result = FloorRequestHandler.StayIdle(Fixture.elevator).Result;

            Assert.False(result.Movement == common.Enums.Movement.Motion);
            Assert.False(result.Direction == common.Enums.Direction.Up);
        }

        [Fact]
        public void SetIdleState_valid()
        {
            var result = FloorRequestHandler.StayIdle(Fixture.elevator).Result;

            Assert.True(result.Movement == common.Enums.Movement.Stationary);
            Assert.True(result.Direction == common.Enums.Direction.Idle);
        }


    }
}