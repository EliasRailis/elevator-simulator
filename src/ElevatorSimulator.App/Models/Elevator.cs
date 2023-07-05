using ElevatorSimulator.App.Models.Enums;

namespace ElevatorSimulator.App.Models;

public sealed class Elevator
{
    public string Id { get; set; }
    
    public Status CurrentStatus { get; set; }
    
    public int FloorNumber { get; set; }
    
    public Elevator(string id, Status currentStatus, int floorNumber)
    {
        Id = id;
        CurrentStatus = currentStatus;
        FloorNumber = floorNumber;
    }
}