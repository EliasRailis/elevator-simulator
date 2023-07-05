﻿using ElevatorSimulator.App.Models;
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
            Console.WriteLine("\n Over the weight limit...");
            return;
        }
        
        Console.WriteLine($"\nElevator {elevator.Id} {elevator.CurrentStatus}");
        _elevator.CallingElevator(elevator);
        
        Console.Write("\nWhich floor are we moving to: ");
        string movingTo = Console.ReadLine() ?? throw new InvalidOperationException();
        _elevator.FloorDestination(elevator, int.Parse(movingTo));
        _elevator.PrintElevatorInformation();
    }

    private List<Person> GeneratingRandomPeople(int count, out double totalWeight)
    {
        var people = new List<Person>();
        var random = new Random();
        for (int i = 0; i < count; i++)
        {
            var person = new Person($"P{i}", random.Next(75, 110 + 1));
            Console.WriteLine(person.ToString());
            people.Add(person);
        }

        totalWeight = people.Sum(x => x.Weight);
        return people;
    }
}