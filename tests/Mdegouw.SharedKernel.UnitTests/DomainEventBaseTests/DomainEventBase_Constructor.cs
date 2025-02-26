﻿using Xunit;
using FluentAssertions;
using Mdegouw.SharedKernel;

namespace Mdegouw.SharedKernel.UnitTests.DomainEventBaseTests;

public class DomainEventBase_Constructor
{
  private class TestDomainEvent : DomainEventBase { }

  [Fact]
  public void SetsDateOccurredToCurrentDateTime()
  {
    // Arrange
    var beforeCreation = DateTime.UtcNow;

    // Act
    var domainEvent = new TestDomainEvent();

    // Assert
    domainEvent.DateOccurred.Should().BeOnOrAfter(beforeCreation);
    domainEvent.DateOccurred.Should().BeOnOrBefore(DateTime.UtcNow);
  }
}
