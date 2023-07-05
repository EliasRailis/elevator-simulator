using ElevatorSimulator.App.Models;
using ElevatorSimulator.App.Models.Enums;
using ElevatorSimulator.App.Repository.Interfaces;

namespace ElevatorSimulator.App.Repository.Implementation;

public sealed class ElevatorRepository : IElevator
{
    private int _numOfFloors = 0;
    private int _numOfElevators = 0;
    private readonly List<Elevator> Elevators = new();

    public void GenerateElevators(int numOfFloors, int numOfElevators)
    {
        _numOfFloors = numOfFloors;
        _numOfElevators = numOfElevators;

        var random = new Random();
        for (int i = 0; i < numOfElevators; i++)
        {
            Elevators.Add(new Elevator($"EL{i}", Status.AVAILABLE, random.Next(0, numOfFloors + 1)));
        }
        
        PrintElevatorInformation();
    }

    public bool ValidAmountOfFloors(int numOfSelectedFloors) => numOfSelectedFloors < _numOfFloors;
    
    public Elevator? GetClosestElevator(int targetFloor)
    {
        Elevator? closestElevator = null;
        int minDifference = int.MaxValue; 
        
        foreach (var elevator in Elevators)
        {
            int difference = Math.Abs(elevator.FloorNumber - targetFloor);
            if (difference < minDifference)
            {
                minDifference = difference;
                closestElevator = elevator;
            }
        }

        if (closestElevator is not null) closestElevator.CurrentStatus = Status.CALLED;
        return closestElevator;
    }

    private void PrintElevatorInformation()
    {
        if (Elevators.Count == 0) return;
        foreach (var elevator in Elevators)
        {
            Console.WriteLine($"\nElevator ID: {elevator.Id}");
            Console.WriteLine($"Status: {elevator.CurrentStatus}");
            Console.WriteLine($"Floor: {elevator.FloorNumber}");
        }
        Console.WriteLine();
    }
}