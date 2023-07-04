
using ElevatorSimulator.App.Models;
using ElevatorSimulator.App.Models.Enums;

Console.Write("Number of floors: ");
string numberOfFloors = Console.ReadLine() ?? throw new InvalidOperationException();

Console.Write("Number of elevators: ");
string numberOfElevators = Console.ReadLine() ?? throw new InvalidOperationException();

IEnumerable<Elevator> listOfElevators = GenerateElevators(int.Parse(numberOfElevators), int.Parse(numberOfFloors));

foreach (var elevator in listOfElevators)
{
    Console.WriteLine(elevator);
}

Console.ReadLine();

IEnumerable<Elevator> GenerateElevators(int numOfElevators, int floors)
{
    var elevators = new List<Elevator>();
    var random = new Random();
    
    for (int i = 0; i < numOfElevators; i++)
    {
        elevators.Add(new Elevator($"EL{i}", Status.STOPPED, random.Next(0, floors + 1)));
    }

    return elevators;
}