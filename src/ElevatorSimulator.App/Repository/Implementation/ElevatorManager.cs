using ElevatorSimulator.App.Models;
using ElevatorSimulator.App.Models.Enums;
using ElevatorSimulator.App.Repository.Interfaces;

namespace ElevatorSimulator.App.Repository.Implementation;

public sealed class ElevatorManager : IElevatorManager
{
    private int _numOfFloors = 0;
    private int _targetFloor = 0;
    private readonly List<Elevator> Elevators = new();

    /// <summary>
    /// Method generates the elevators used in the application
    /// </summary>
    /// <param name="numOfFloors">The number of floors</param>
    /// <param name="numOfElevators"></param>
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

    /// <summary>
    /// Method used to check if the requested floor is in the range of available floors
    /// in the building
    /// </summary>
    /// <param name="numOfSelectedFloors">Number of selected floors</param>
    /// <returns>Boolean response</returns>
    public bool ValidAmountOfFloors(int numOfSelectedFloors)
    {
        return numOfSelectedFloors <= _numOfFloors;
    }

    /// <summary>
    /// The method will check the weight limit of all the people in the request and will
    /// check if it's over the weight limit of 500.00kg 
    /// </summary>
    /// <param name="totalWeightOfPeople">Total weight of people</param>
    /// <param name="elevator"><see cref="Elevator"/> nearest elevator object</param>
    /// <returns>Boolean response</returns>
    public bool CheckWeightLimit(double totalWeightOfPeople, Elevator elevator) 
    {
        return totalWeightOfPeople < elevator.WeightLimit;
    }

    /// <summary>
    /// Method will get the closest elevator from the list of elevators requested from the used
    /// </summary>
    /// <param name="targetFloor">The target floor</param>
    /// <param name="numberOfPeople">Number of people</param>
    /// <returns>Nullable object of type <see cref="Elevator"/></returns>
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

    /// <summary>
    /// Moving the elevator to the requested floor
    /// </summary>
    /// <param name="elevator"><see cref="Elevator"/> object</param>
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

    /// <summary>
    /// Moving the elevator to the destination floor requested from the user
    /// </summary>
    /// <param name="elevator"><see cref="Elevator"/> object</param>
    /// <param name="floorDestination">Floor destination</param>
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

    /// <summary>
    /// Method moves the elevator up 
    /// </summary>
    /// <param name="elevator"><see cref="Elevator"/> object</param>
    /// <param name="floor">Floor number</param>
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

    /// <summary>
    /// Method moves the elevator down
    /// </summary>
    /// <param name="elevator"><see cref="Elevator"/> object</param>
    /// <param name="floor">Floor number</param>
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

    /// <summary>
    /// User the method to print out the generated elevators
    /// </summary>
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