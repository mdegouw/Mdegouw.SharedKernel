﻿using FluentAssertions;
using Mdegouw.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace Mdegouw.SharedKernel.UnitTests.MediatRDomainEventDispatcherTests;

public class DispatchAndClearEventsWithGuidId
{
  private class TestDomainEvent : DomainEventBase { }
  private class TestEntity : EntityBase<Guid>
  {
    public void AddTestDomainEvent()
    {
      var domainEvent = new TestDomainEvent();
      RegisterDomainEvent(domainEvent);
    }
  }

  [Fact]
  public async Task CallsPublishAndClearDomainEvents()
  {
    // Arrange
    var mediatorMock = new Mock<IMediator>();
    var domainEventDispatcher = new MediatRDomainEventDispatcher(mediatorMock.Object, NullLogger<MediatRDomainEventDispatcher>.Instance);
    var entity = new TestEntity();
    entity.AddTestDomainEvent();

    // Act
    await domainEventDispatcher.DispatchAndClearEvents(new List<EntityBase<Guid>> { entity });

    // Assert
    mediatorMock.Verify(m => m.Publish(It.IsAny<DomainEventBase>(), It.IsAny<CancellationToken>()), Times.Once);
    entity.DomainEvents.Should().BeEmpty();
  }
}
