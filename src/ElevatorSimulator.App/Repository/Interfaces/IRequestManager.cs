namespace ElevatorSimulator.App.Repository.Interfaces;

public interface IRequestManager
{
    void GenerateRequest(int numOfPeople, int targetFloor);
}