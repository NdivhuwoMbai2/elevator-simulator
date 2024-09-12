namespace elevator_simulator.common.Enums
{
    /// <summary>
    /// movement enums will be used to point out whether the elevator is currently moving or staionary
    /// </summary>
    public enum Movement
    {
        Motion,
        Stationary
    }
    /// <summary>
    /// Used to point out the direction of the elevator
    /// </summary>
    public enum Direction
    {
        Up,
        Down,
        Idle
    }
    public enum Boarding
    {
        In,
        Out
    }
}
