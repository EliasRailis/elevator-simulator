using ElevatorSimulator.App;
using ElevatorSimulator.App.Repository.Implementation;
using ElevatorSimulator.App.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .AddSingleton<IRequest, RequestRepository>()
    .AddSingleton<IElevator, ElevatorRepository>()
    .BuildServiceProvider();

Print.GetMainInput(out string floors, out string elevators);

var _elevatorService = services.GetRequiredService<IElevator>();
var _requestsService = services.GetRequiredService<IRequest>();
_elevatorService.GenerateElevators(int.Parse(floors), int.Parse(elevators));

bool continueLoop = true;
while (continueLoop)
{
    Print.MakeRequestInput(out string answer);

    if (answer.ToUpper() == "Y")
    {
        Print.GetAmountsInputs(out string amountOfPeople, out string targetFloor);

        if (!_elevatorService.ValidAmountOfFloors(int.Parse(targetFloor)))
        {
            Print.PrintError("Invalid amount of floors...");
            continue;
        }
        
        _requestsService.GenerateRequest(int.Parse(amountOfPeople), int.Parse(targetFloor));
        Console.WriteLine();
    }
    else if (answer.ToUpper() == "N")
    {
        Console.WriteLine("Exiting...");
        Thread.Sleep(500);
        continueLoop = !continueLoop;
    }
}