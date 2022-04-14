using Ardalis.Specification;
using DDDPracticeDomainEvents.Core.ProjectAggregate;

namespace DDDPracticeDomainEvents.Core.ProjectAggregate.Specifications;

public class ProjectByIdWithItemsSpec : Specification<Project>, ISingleResultSpecification
{
  public ProjectByIdWithItemsSpec(int projectId)
  {
    Query
        .Where(project => project.Id == projectId)
        .Include(project => project.Items);
  }
}
