using ElevatorSimulator.App.Repository.Implementation;
using ElevatorSimulator.App.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .AddSingleton<IRequest, RequestRepository>()
    .AddSingleton<IElevator, ElevatorRepository>()
    .BuildServiceProvider();

Console.Write("Number of Floors: ");
string floors = Console.ReadLine() ?? throw new InvalidOperationException();

Console.Write("Number of Elevators: ");
string elevators = Console.ReadLine() ?? throw new InvalidOperationException();

var _elevatorService = services.GetRequiredService<IElevator>();
var _requestsService = services.GetRequiredService<IRequest>();
_elevatorService.GenerateElevators(int.Parse(floors), int.Parse(elevators));

bool continueLoop = true;
while (continueLoop)
{
    Console.Write("Would you like to make a request (y/n): ");
    string answer = Console.ReadLine() ?? throw new InvalidOperationException();

    if (answer.ToUpper() == "N")
    {
        Console.WriteLine("Exiting...");
        Thread.Sleep(500);
        continueLoop = !continueLoop;
        continue;
    }
    
    Console.Write("Amount of people: ");
    string amountOfPeople = Console.ReadLine() ?? throw new InvalidOperationException();
    
    Console.Write("Target floor: ");
    string targetFloor = Console.ReadLine() ?? throw new InvalidOperationException();

    if (!_elevatorService.ValidAmountOfFloors(int.Parse(targetFloor)))
    {
        Console.WriteLine("\nInvalid amount of floors...");
        continue;
    }
    
    _requestsService.GenerateRequest(int.Parse(amountOfPeople), int.Parse(targetFloor));
    Console.WriteLine();
}