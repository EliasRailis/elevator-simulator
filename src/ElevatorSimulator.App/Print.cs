namespace ElevatorSimulator.App;

public static class Print 
{
    public static void GetMainInput(out string floorsInput, out string elevatorsInput)
    {
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Cyan; 
        Console.Write("1");
        Console.ResetColor();
        Console.Write("] ");
        Console.Write("Number of Floors: ");
        floorsInput = Console.ReadLine() ?? throw new InvalidOperationException();

        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Cyan; 
        Console.Write("2");
        Console.ResetColor();
        Console.Write("] ");
        Console.Write("Number of Elevators: ");
        elevatorsInput = Console.ReadLine() ?? throw new InvalidOperationException();
    }

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

    public static void GetAmountsInputs(out string amountOfPeopleInput, out string targetFloorInput)
    {
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Cyan; 
        Console.Write("4");
        Console.ResetColor();
        Console.Write("] ");
        Console.Write("Amount of people: ");
        amountOfPeopleInput = Console.ReadLine() ?? throw new InvalidOperationException();
        
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Cyan; 
        Console.Write("5");
        Console.ResetColor();
        Console.Write("] ");
        Console.Write("On Floor (The floor where the people are): ");
        targetFloorInput = Console.ReadLine() ?? throw new InvalidOperationException();
    }
    
    public static void MoveToInput(out string movingTo)
    {
        Console.Write("\n[");
        Console.ForegroundColor = ConsoleColor.Cyan; 
        Console.Write("6");
        Console.ResetColor();
        Console.Write("] ");
        Console.Write("Destination floor: ");
        movingTo = Console.ReadLine() ?? throw new InvalidOperationException();
    }

    public static void PrintError(string message)
    {
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Red; 
        Console.Write("ERROR");
        Console.ResetColor();
        Console.Write("] ");
        Console.WriteLine($"{message}");
    }
}