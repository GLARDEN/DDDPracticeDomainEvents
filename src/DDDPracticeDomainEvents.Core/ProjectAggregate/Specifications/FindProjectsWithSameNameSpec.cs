using System.Data;

using Ardalis.Specification;

using DDDPracticeDomainEvents.Core.ProjectAggregate;

namespace DDDPractice.DomainEvents.Core.ProjectAggregate.Specifications;

public class FindProjectsWithSameNameSpec : Specification<Project>
{
  public FindProjectsWithSameNameSpec(int projectId, string projectName)
  {
    Query
        .Where(project => project.Id != projectId &&
          project.Name.ToLower() == projectName.ToLower());
  }
}
