# DDD Practice project - Implement Domain Entity Events

Created new project using [Clean Architecture](https://github.com/ardalis/apiendpoints) Visual Studio template as a test project to practice implementing Example 11 from 
[DDD NoDuplicates](https://github.com/ardalis/DDD-NoDuplicate) in a .net core 6 web project.

## Implementation Questions
1) We need a static instance of Mediator which is implemented by creating a static DomainEvents Class
```cs
    public static class DomainEvents
    {
      public static Func<IMediator> Mediator;

      public static async Task Raise<T>(T args) where T : INotification
      {
        var mediator = Mediator.Invoke();
        await mediator.Publish(args);
      }
    }
```
2) Setting up Mediator in Program.cs (.net core 6 Single file set up) 
   
```cs
      //Set up a static instance of mediator to handle Immediate Domain Events.
      SetUpMediatR(app);

      app.Run();

      void SetUpMediatR(IApplicationBuilder app)
      {
        var serviceProvider = app.ApplicationServices;
        DomainEvents.Mediator = () => BuildMediator(serviceProvider);
      }

      IMediator BuildMediator(IServiceProvider serviceProvider)
      {
        return serviceProvider.GetRequiredService<IMediator>();
      }
```

## Reference Projects
- [Clean Architecture](https://github.com/ardalis/apiendpoints)
- [DDD NoDuplicates](https://github.com/ardalis/DDD-NoDuplicate)
- [ApiEndpoints](https://github.com/ardalis/apiendpoints)
- [GuardClauses](https://github.com/ardalis/guardclauses)
- [Result](https://github.com/ardalis/result)
- [Specification](https://github.com/ardalis/specification)

