using ElevatorSimulator.App.Models;
using ElevatorSimulator.App.Repository.Interfaces;

namespace ElevatorSimulator.App.Repository.Implementation;

public class RequestRepository : IRequest
{
    private readonly IElevator _elevator;

    public RequestRepository(IElevator elevator) => _elevator = elevator;

    public void GenerateRequest(int numOfPeople, int targetFloor)
    {
        List<Person> people = GeneratingRandomPeople(numOfPeople);
        Elevator? elevator = _elevator.GetClosestElevator(targetFloor);
        
        if (elevator is null) return;
        
        Console.WriteLine($"Elevator {elevator.Id} {elevator.CurrentStatus}");
    }

    private List<Person> GeneratingRandomPeople(int count)
    {
        var people = new List<Person>();
        for (int i = 0; i < count; i++)
        {
            people.Add(new Person($"P{i}", 75));
        }
        return people;
    }
}