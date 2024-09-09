using elevator_simulator.common.Enums;

namespace elevator_simulator.common.v1.Models
{
    public class Status
    {
        public int currentFloor { get; set; }
        public Direction Direction { get; set; } = Direction.Idle;
        public int PassengerCount { get; set; }
        public Movement Movement { get; set; } = Movement.Stationary;

    }
}
