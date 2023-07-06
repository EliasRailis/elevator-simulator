using ElevatorSimulator.App.Models;
using ElevatorSimulator.App.Repository.Interfaces;

namespace ElevatorSimulator.App.Repository.Implementation;

public class RequestManager : IRequestManager
{
    private readonly IElevatorManager _elevatorManager;

    public RequestManager(IElevatorManager elevator) => _elevatorManager = elevator;

    public void GenerateRequest(int numOfPeople, int targetFloor)
    {
        List<Person> people = GeneratingRandomPeople(numOfPeople, out double totalPeopleWeight); 
        Elevator? elevator = _elevatorManager.GetClosestElevator(targetFloor, people.Count);

        if (elevator is null) return;
        
        bool isInWeightLimit = _elevatorManager.CheckWeightLimit(totalPeopleWeight, elevator);
        
        if (!isInWeightLimit)
        {
            Print.PrintError("Over the weight limit...");
            return;
        }
        
        Console.WriteLine($"\nElevator selected {elevator.Id} {elevator.CurrentStatus}");
        _elevatorManager.CallingElevator(elevator);
        
        Print.MoveToInput(out string movingTo);
        _elevatorManager.FloorDestination(elevator, int.Parse(movingTo));
        _elevatorManager.PrintElevatorInformation();
    }

    /// <summary>
    /// Method will generate the people data that will enter the elevator
    /// </summary>
    /// <param name="count">Number of people</param>
    /// <param name="totalWeight">Will return the total weight of the people</param>
    /// <returns>A list object of type <see cref="Person"/></returns>
    private List<Person> GeneratingRandomPeople(int count, out double totalWeight)
    {
        Console.WriteLine("\nGenerating people...");
        var people = new List<Person>();
        var random = new Random();
        
        for (int i = 0; i < count; i++)
        {
            var person = new Person($"P{i}", random.Next(75, 110 + 1));
            Console.ForegroundColor = ConsoleColor.Yellow; 
            Console.WriteLine(person.ToString());
            Console.ResetColor();
            people.Add(person);
        }

        totalWeight = people.Sum(x => x.Weight);
        return people;
    }
}