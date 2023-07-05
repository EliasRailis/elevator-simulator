namespace ElevatorSimulator.App.Repository.Interfaces;

public interface IRequest
{
    void GenerateRequest(int numOfPeople, int targetFloor);
}