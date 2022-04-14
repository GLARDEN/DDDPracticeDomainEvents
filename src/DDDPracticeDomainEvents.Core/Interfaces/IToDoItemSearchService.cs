using Ardalis.Result;
using DDDPracticeDomainEvents.Core.ProjectAggregate;

namespace DDDPracticeDomainEvents.Core.Interfaces;

public interface IToDoItemSearchService
{
  Task<Result<ToDoItem>> GetNextIncompleteItemAsync(int projectId);
  Task<Result<List<ToDoItem>>> GetAllIncompleteItemsAsync(int projectId, string searchString);
}
