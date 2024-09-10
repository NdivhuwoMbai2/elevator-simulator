using elevator_simulator.common.v1.Interfaces;
using elevator_simulator.tests.Fixture;

namespace elevator_simulator.tests
{

    public class FloorRequestHandler_Tests : IClassFixture<TestDataFixture>
    {

        public IFloorRequestHandler FloorRequestHandler; 
        public TestDataFixture Fixture;
        public FloorRequestHandler_Tests(IFloorRequestHandler FloorRequestHandler,  TestDataFixture Fixture)
        {
            this.FloorRequestHandler = FloorRequestHandler;
            this.Fixture = Fixture; 
        }
        [Theory]
        [InlineData(2, 5)]
        public void Decend_input_2floor_returns_invalidvalid(int currentFloor, int requestedFloor)
        {
            //arrange 
            int expected = 3;
            Fixture.elevator.currentFloor = currentFloor;

            //act
            var actual = FloorRequestHandler.Descend(Fixture.elevator, requestedFloor)?.Result?.currentFloor;

            //assert
            Assert.NotEqual(expected, actual);
        }
        [Theory]
        [InlineData(5, 2)]
        public void Decend_input_2floor_returns_valid(int currentFloor, int requestedFloor)
        {
            //arrange 
            int expected = 2;
            Fixture.elevator.currentFloor = currentFloor;

            //act
            var actual = FloorRequestHandler.Descend(Fixture.elevator, requestedFloor).Result;

            //assert
            Assert.Equal(expected, actual.currentFloor);
            Assert.Empty(actual.ErrorMessage);
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