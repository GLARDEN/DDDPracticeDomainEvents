
using MediatR;

namespace DDDPracticeDomainEvents.SharedKernel;

public static class DomainEvents
{
  public static Func<IMediator> Mediator;

  public static async Task Raise<T>(T args) where T : INotification
  {
    var mediator = Mediator.Invoke();
    await mediator.Publish(args);
  }
}

