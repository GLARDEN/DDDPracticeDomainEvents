using Autofac;
using DDDPracticeDomainEvents.Core.Interfaces;
using DDDPracticeDomainEvents.Core.Services;

namespace DDDPracticeDomainEvents.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<ToDoItemSearchService>()
        .As<IToDoItemSearchService>().InstancePerLifetimeScope();
  }
}
