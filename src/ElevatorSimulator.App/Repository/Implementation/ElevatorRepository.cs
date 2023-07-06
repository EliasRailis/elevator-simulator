using ElevatorSimulator.App.Models;
using ElevatorSimulator.App.Models.Enums;
using ElevatorSimulator.App.Repository.Interfaces;

namespace ElevatorSimulator.App.Repository.Implementation;

public sealed class ElevatorRepository : IElevator
{
    private int _numOfFloors = 0;
    private int _targetFloor = 0;
    private readonly List<Elevator> Elevators = new();

    public void GenerateElevators(int numOfFloors, int numOfElevators)
    {
        _numOfFloors = numOfFloors;

        var random = new Random();
        for (int i = 0; i < numOfElevators; i++)
        {
            Elevators.Add(new Elevator($"EL{i}", Status.AVAILABLE, random.Next(0, numOfFloors + 1)));
        }
        
        PrintElevatorInformation();
    }

    public bool ValidAmountOfFloors(int numOfSelectedFloors)
    {
        return numOfSelectedFloors <= _numOfFloors;
    }

    public bool CheckWeightLimit(double totalWeightOfPeople, Elevator elevator) 
    {
        return totalWeightOfPeople < elevator.WeightLimit;
    }

    public Elevator? GetClosestElevator(int targetFloor, int numberOfPeople)
    {
        Elevator? closestElevator = null;
        _targetFloor = targetFloor;
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

        if (closestElevator is not null)
        {
            closestElevator.CurrentStatus = Status.CALLED;
            closestElevator.NumberOfPeople = numberOfPeople;
        }
        
        return closestElevator;
    }

    public void CallingElevator(Elevator elevator)
    {
        switch (elevator.FloorNumber)
        {
            case var val when val > _targetFloor:
                MovingDown(elevator, _targetFloor);
                Console.WriteLine($"Elevator is on floor {elevator.FloorNumber}");
                break;
            case var val when val < _targetFloor:
                MovingUp(elevator, _targetFloor);
                Console.WriteLine($"Elevator is on floor {elevator.FloorNumber}");
                break;
            default:
                Console.WriteLine("Elevator in current floor");
                break;
        }
    }

    public void FloorDestination(Elevator elevator, int floorDestination)
    {
        Console.WriteLine($"There are {elevator.NumberOfPeople} people in the elevator");
        switch (elevator.FloorNumber)
        {
            case var val when val > floorDestination:
                MovingDown(elevator, floorDestination);
                break;
            case var val when val < floorDestination:
                MovingUp(elevator, floorDestination);
                break;
            default:
                Console.WriteLine("Elevator in current floor");
                break;
        }

        elevator.CurrentStatus = Status.AVAILABLE;
        Thread.Sleep(500);
        Console.WriteLine("People exiting the elevator...");
        Thread.Sleep(500);
    }

    private static void MovingUp(Elevator elevator, int floor)
    {
        int diff = floor - elevator.FloorNumber;
        for (int i = 0; i < diff; i++)
        {
            Thread.Sleep(500);
            Console.WriteLine($"Elevator going {Status.UP}");
            Thread.Sleep(500);
            elevator.FloorNumber++;
        }
    }

    private static void MovingDown(Elevator elevator, int floor)
    {
        int diff = elevator.FloorNumber - floor;
        for (int i = 0; i < diff; i++)
        {
            Thread.Sleep(500);
            Console.WriteLine($"Elevator going {Status.DOWN}");
            Thread.Sleep(500);
            elevator.FloorNumber--;
        }
    }

    public void PrintElevatorInformation()
    {
        if (Elevators.Count == 0) return;
        foreach (var elevator in Elevators)
        {
            Console.ForegroundColor = ConsoleColor.Yellow; 
            Console.WriteLine($"\nElevator ID: {elevator.Id}");
            Console.WriteLine($"Status: {elevator.CurrentStatus}");
            Console.WriteLine($"Floor: {elevator.FloorNumber}");
            Console.ResetColor();
        }
        Console.WriteLine();
    }
}