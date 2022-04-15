namespace DDDPracticeDomainEvents.Core.ProjectAggregate.Exceptions;

public class DuplicateProjectNameException : Exception
{
  public DuplicateProjectNameException(string message) : base(message)
  {


  }
}
