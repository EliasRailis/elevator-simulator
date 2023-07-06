# How it works

## The `Program.cs` file

It’s the main entry of the application.

First I register the services. I used the **`Singleton`** lifetime scope because I wanted the instance to last the entire lifetime of the application.

Once I get the inputs regarding the number of floors, and elevators in the building, I then generate the elevators with the following.

```csharp
_elevatorService.GenerateElevators(int.Parse(floors), int.Parse(elevators));
```

Next the application will go in a loop. You will be asked to enter input if you would like to make a request to be processed.

If you enter “N**”** then the application will exit at that point.

If you enter “Y” then it will ask you to enter two inputs, the number of people that will enter the elevator and your current floor.

```csharp
Print.GetAmountsInputs(out string amountOfPeople, out string targetFloor);
```

The application will then validate that the floor number you entered is in the range of available floors in the building.

```csharp
if (!_elevatorService.ValidAmountOfFloors(int.Parse(targetFloor)))
{
    Print.PrintError("Invalid amount of floors...");
    continue;
}
```

If it’s valid it will generate a request.

```csharp
_requestsService.GenerateRequest(int.Parse(amountOfPeople), int.Parse(targetFloor));
```

## The `Print.cs` file

This file contains static methods for printing out inputs in the console. I decided to go with this approach to keep things clean as much as possible. An example looks as follows:

```csharp
public static void MakeRequestInput(out string makeRequestInput)
{
    Console.Write("[");
    Console.ForegroundColor = ConsoleColor.Cyan; 
    Console.Write("3");
    Console.ResetColor();
    Console.Write("] ");
    Console.Write("Would you like to make a request (y/n): ");
    makeRequestInput = Console.ReadLine() ?? throw new InvalidOperationException();
}
```

## The `RequestManager.cs` file

This file will process the request for calling an elevator. Firstly, it will generate a list of People object all containing a Name and Weight. 

The `Person.cs` class.

```csharp
public record Person(string Name, double Weight);
```

Next it will get the nearest elevator to the requested floor.

Once the elevator has been found the application will check that the combined weight of the people doesn’t exceed the weight limit of the elevator, which is 500.00kg.

```csharp
bool isInWeightLimit = _elevatorManager.CheckWeightLimit(totalPeopleWeight, elevator);
```

If the weight limit is exceeded the application will restart.

When the closest elevator is found, the app will call the elevator to the requested floor.

```csharp
_elevatorManager.CallingElevator(elevator);
```

Next you will be asked to enter the input for the floor you would like to get to. Once entered,

the process of moving the elevator to the requested floor.

```csharp
Print.MoveToInput(out string movingTo);
_elevatorManager.FloorDestination(elevator, int.Parse(movingTo));
```

## The `ElevatorManager.cs` file

This file contains mostly the methods to perform the elevator actions, like generating the elevators.

```csharp
public void GenerateElevators(int numOfFloors, int numOfElevators)
{
    _numOfFloors = numOfFloors;

    var random = new Random();
    for (int i = 0; i < numOfElevators; i++)
    {
        _elevators.Add(new Elevator($"EL{i}", Status.AVAILABLE, 
														random.Next(0, numOfFloors + 1)));
    }
    
    PrintElevatorInformation();
}
```

Validating the amount of floors in the building, checking the weight limit on the elevator.

```csharp
public bool ValidAmountOfFloors(int numOfSelectedFloors)
{
    return numOfSelectedFloors <= _numOfFloors;
}

// AND

public bool CheckWeightLimit(double totalWeightOfPeople, Elevator elevator) 
{
    return totalWeightOfPeople < elevator.WeightLimit;
}
```

And moving the elevator up or down the floors in the building.

```csharp
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

// AND

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
```