using DDDPracticeDomainEvents.Core.ProjectAggregate;
using DDDPracticeDomainEvents.SharedKernel;

namespace DDDPracticeDomainEvents.Core.ProjectAggregate.Events;

public class ToDoItemCompletedEvent : BaseDomainEvent
{
  public ToDoItem CompletedItem { get; set; }

  public ToDoItemCompletedEvent(ToDoItem completedItem)
  {
    CompletedItem = completedItem;
  }
}
