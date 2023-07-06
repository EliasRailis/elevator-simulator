using ElevatorSimulator.App.Models.Enums;

namespace ElevatorSimulator.App.Models;

public sealed class Elevator
{
    public string Id { get; set; }
    
    public Status CurrentStatus { get; set; }
    
    public int FloorNumber { get; set; }

    public double WeightLimit { get; set; } = 650.00;

    public int NumberOfPeople { get; set; } = 0;
    
    public Elevator(string id, Status currentStatus, int floorNumber)
    {
        Id = id;
        CurrentStatus = currentStatus;
        FloorNumber = floorNumber;
    }
}