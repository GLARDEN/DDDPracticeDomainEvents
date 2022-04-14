using Ardalis.Specification;

namespace DDDPracticeDomainEvents.SharedKernel.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
