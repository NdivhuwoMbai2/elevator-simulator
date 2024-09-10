using elevator_simulator.common.v1.Interfaces;
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
            var result = ElevatorHandler.Add(Fixture.elevator, Fixture.Elevators);
            Assert.True(result.Count > 0);
        }
        [Fact]
        public void Add_elevator_inValid()
        {
            var result = ElevatorHandler.Add(null, new List<common.v1.Models.Elevator>() { });
            Assert.False(result.Count > 0);
        }
        [Fact]
        public void Get_elevator_with_space_valid()
        {
            var result = ElevatorHandler.GetElevatorWithSpace(Fixture.request, Fixture.Elevators).Result;
            Assert.NotNull(result);
        }
        [Fact]
        public void Get_elevator_with_space_invalid()
        {
            var result = ElevatorHandler.GetElevatorWithSpace(
                new common.v1.Models.Request() { NumberOfPassengers = 11 },
                new List<common.v1.Models.Elevator>() { new common.v1.Models.Elevator { MaximumCapacity = 10 } }).Result;
            Assert.True(result.Count == 0);
        }
    }
}
