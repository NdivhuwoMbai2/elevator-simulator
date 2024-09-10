using elevator_simulator.common.v1.Interfaces;
using elevator_simulator.tests.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elevator_simulator.tests
{
    public class ElevatorRepo : IClassFixture<TestDataFixture>
    {
        public IFloorRequestHandler FloorRequestHandler;
        public IElevatorRepository ElevatorRepository;
        public TestDataFixture Fixture;
        public ElevatorRepo(IFloorRequestHandler FloorRequestHandler, IElevatorRepository ElevatorRepository, TestDataFixture Fixture)
        {
            this.FloorRequestHandler = FloorRequestHandler;
            this.Fixture = Fixture;
            this.ElevatorRepository = ElevatorRepository;
        }
        [Fact]
        public void Get_elevator_with_space_valid()
        {
            var res = Fixture.elevator;
            var result = ElevatorRepository.GetElevatorWithSpace(Fixture.request, Fixture.Elevators).Result;
            Assert.NotNull(result);
        }
        [Fact]
        public void Get_elevator_with_space_invalid()
        { 
            var result = ElevatorRepository.GetElevatorWithSpace(
                new common.v1.Models.Request() { NumberOfPassengers = 11 },
                new List<common.v1.Models.Elevator>() { new common.v1.Models.Elevator { MaximumCapacity = 10 } }).Result;
            Assert.True(result.Count==0);
        }
    }
}
