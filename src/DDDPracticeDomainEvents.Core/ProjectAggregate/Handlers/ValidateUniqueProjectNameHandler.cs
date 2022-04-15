
using DDDPractice.DomainEvents.Core.ProjectAggregate.Specifications;

using DDDPracticeDomainEvents.Core.ProjectAggregate.Events;
using DDDPracticeDomainEvents.Core.ProjectAggregate.Exceptions;
using DDDPracticeDomainEvents.SharedKernel.Interfaces;

using MediatR;

namespace DDDPracticeDomainEvents.Core.ProjectAggregate.Handlers;

public class ProjectNameChangeHandler : INotificationHandler<ProjectNameChangeRequested>
{
  private readonly IRepository<Project> _repository;

  public ProjectNameChangeHandler(IRepository<Project> repository)
  {
    _repository = repository;
  }

  public async Task Handle(ProjectNameChangeRequested notification, CancellationToken cancellationToken)
  {
    if(notification.Id == 1)
    {
      Thread.Sleep(10000);
    }

    var findProjectsWithSameNameSpec = new FindProjectsWithSameNameSpec(notification.Id, notification.NewName);

    var foundDuplicateRoleName = await _repository.AnyAsync(findProjectsWithSameNameSpec);

    if (foundDuplicateRoleName)
    {
      throw new DuplicateProjectNameException($"{notification.NewName} already exists.");
    }
  }
}
