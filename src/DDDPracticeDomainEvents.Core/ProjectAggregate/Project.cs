using Ardalis.GuardClauses;

using DDDPracticeDomainEvents.Core.ProjectAggregate.Events;
using DDDPracticeDomainEvents.SharedKernel;
using DDDPracticeDomainEvents.SharedKernel.Interfaces;

namespace DDDPracticeDomainEvents.Core.ProjectAggregate;

public class Project : BaseEntity, IAggregateRoot
{
  public string Name { get; private set; }

  private List<ToDoItem> _items = new List<ToDoItem>();
  public IEnumerable<ToDoItem> Items => _items.AsReadOnly();
  public ProjectStatus Status => _items.All(i => i.IsDone) ? ProjectStatus.Complete : ProjectStatus.InProgress;

  public PriorityStatus Priority { get; }

  public Project(string name, PriorityStatus priority)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Priority = priority;
  }

  public void AddItem(ToDoItem newItem)
  {
    Guard.Against.Null(newItem, nameof(newItem));
    _items.Add(newItem);

    var newItemAddedEvent = new NewItemAddedEvent(this, newItem);
    Events.Add(newItemAddedEvent);
  }

  public void UpdateName(string newName)
  {
    Guard.Against.NullOrEmpty(newName, nameof(newName));
    DomainEvents.Raise(new ProjectNameChangeRequested(Id, newName)).GetAwaiter().GetResult();
    Name = newName;
  }
}
