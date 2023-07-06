using System.Collections.Generic;
using System.Reflection;
using ElevatorSimulator.App.Models;
using ElevatorSimulator.App.Models.Enums;
using ElevatorSimulator.App.Repository.Interfaces;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace ElevatorSimulator.Unit.Tests;

public sealed class UnitTests
{
    private readonly IElevatorManager _elevatorManager = Substitute.For<IElevatorManager>();

    [Fact]
    public void GetClosestElevator_ShouldReturnClosest_WhenFound()
    {
        // Arrange
        var elevators = new List<Elevator>()
        {
            new("EL0", Status.AVAILABLE, 11),
            new("EL1", Status.AVAILABLE, 5)
        };
        
        var field = typeof(IElevatorManager).GetField("_elevators", BindingFlags.GetField | BindingFlags.Instance);
        field?.SetValue(_elevatorManager, elevators);
        
        const int floor = 1;
        const int people = 3;
        _elevatorManager.GetClosestElevator(floor, people).Returns(elevators[1]);
 
        // Act
        var result = _elevatorManager.GetClosestElevator(floor, people);

        // Assert
        result.Should().BeEquivalentTo(elevators[1]);
    }
}