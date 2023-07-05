using ElevatorSimulator.App.Models;

namespace ElevatorSimulator.App.Repository.Interfaces;

public interface IElevator
{
    void GenerateElevators(int numOfFloors, int numOfElevators);

    bool ValidAmountOfFloors(int numOfSelectedFloors);

    bool CheckWeightLimit(double totalWeightOfPeople, Elevator elevator);

    Elevator? GetClosestElevator(int targetFloor, int numberOfPeople);

    void CallingElevator(Elevator elevator);

    void FloorDestination(Elevator elevator, int floorDestination);
    
    void PrintElevatorInformation();
}