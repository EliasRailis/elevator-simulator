using ElevatorSimulator.App.Models;

namespace ElevatorSimulator.App.Repository.Interfaces;

public interface IElevator
{
    void GenerateElevators(int numOfFloors, int numOfElevators);

    bool ValidAmountOfFloors(int numOfSelectedFloors);

    Elevator? GetClosestElevator(int targetFloor);
}