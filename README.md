<b>DDD Practice project - Implement Domain Entity Events</b>

Created new project using [Clean Architecture](https://github.com/ardalis/apiendpoints) Visual Studio template as a test project to practice implementing Example 11 from 
[DDD NoDuplicates](https://github.com/ardalis/DDD-NoDuplicate) in a .net core 6 web project.

<b>Implementation Highlights</b> 

<i>Code below is provided by sample projects below.</i>



1) Method from Project class that calls the Domain Event to validate the requested project name is unique.
```cs
  public void UpdateName(string newName)
  {
    Guard.Against.NullOrEmpty(newName, nameof(newName));
    DomainEvents.Raise(new ProjectNameChangeRequested(Id, newName)).GetAwaiter().GetResult();
    Name = newName;
  }
```
<b>QUESTION</b>

2) Setting up Mediator in Program.cs (.net core 6 single file set up). SetUpMediatR method was changed to use
   IApplicationBuilder to get the application services.
   
  <b>NOTE:</b> I'm not sure if this is correct! It seems to work great but I'm not sure I fully understand how the GetRequiredService call 
               from IApplicationBuilder works. I believe <i>serviceProvider.GetRequiredService\<IMediator\>();</i> will return
               a unique instance of the service per request.
  
  In the <i>ProjectNameChangeHandler</i> I added a delay to the validation for a specifc project id to make sure this was not a blocking call. 
  I opened 2 instances of swagger and triggered  Product.UpdateName for both id 1 & 2. I was able to call Product.UpdateName for ID 2 while 
  the validation for the ID 1 was sleeping. SUCCESS!  
  
```cs
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
```  
```cs
      //Set up a static instance of mediator to handle Immediate Domain Events.
      //Program.cs truncated for readability
  
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
3) We need a static instance of Mediator which is implemented by creating a static DomainEvents Class
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
<b>Reference Projects</b>
- [Clean Architecture](https://github.com/ardalis/apiendpoints)
- [DDD NoDuplicates](https://github.com/ardalis/DDD-NoDuplicate)
- [ApiEndpoints](https://github.com/ardalis/apiendpoints)
- [GuardClauses](https://github.com/ardalis/guardclauses)
- [Result](https://github.com/ardalis/result)
- [Specification](https://github.com/ardalis/specification)

