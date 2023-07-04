using ElevatorSimulator.App.Models.Enums;

namespace ElevatorSimulator.App.Models;

public record Elevator(string Id, Status CurrentStatus, int FloorNumber);