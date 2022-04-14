
using MediatR;

namespace DDDPracticeDomainEvents.Core.ProjectAggregate.Events;

public class ProjectNameChangeRequested : INotification
{
  public ProjectNameChangeRequested(int id, string newName)
  {
    Id = id;
    NewName = newName;
  }

  public int Id { get; }
  public string NewName { get; }

}
