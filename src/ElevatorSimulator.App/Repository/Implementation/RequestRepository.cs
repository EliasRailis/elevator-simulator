using ElevatorSimulator.App.Models;
using ElevatorSimulator.App.Repository.Interfaces;

namespace ElevatorSimulator.App.Repository.Implementation;

public class RequestRepository : IRequest
{
    private readonly IElevator _elevator;

    public RequestRepository(IElevator elevator) => _elevator = elevator;

    public void GenerateRequest(int numOfPeople, int targetFloor)
    {
        List<Person> people = GeneratingRandomPeople(numOfPeople, out double totalPeopleWeight); 
        Elevator? elevator = _elevator.GetClosestElevator(targetFloor, people.Count);

        if (elevator is null) return;
        
        bool isInWeightLimit = _elevator.CheckWeightLimit(totalPeopleWeight, elevator);
        
        if (!isInWeightLimit)
        {
            Print.PrintError("Over the weight limit...");
            return;
        }
        
        Console.WriteLine($"\nElevator selected {elevator.Id} {elevator.CurrentStatus}");
        _elevator.CallingElevator(elevator);
        
        Print.MoveToInput(out string movingTo);
        _elevator.FloorDestination(elevator, int.Parse(movingTo));
        _elevator.PrintElevatorInformation();
    }

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