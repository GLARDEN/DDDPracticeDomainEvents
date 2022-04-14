
using DDDPracticeDomainEvents.Core.ProjectAggregate;
using DDDPracticeDomainEvents.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace DDDPracticeDomainEvents.Web;

public static class SeedData
{
  public static readonly Project TestProject1 = new Project("Test Project 1", PriorityStatus.Backlog);
  public static readonly ToDoItem ToDoItem1 = new ToDoItem
  {
    Title = "Get Sample Working",
    Description = "Try to get the sample to build."
  };
  public static readonly ToDoItem ToDoItem2 = new ToDoItem
  {
    Title = "Review Solution",
    Description = "Review the different projects in the solution and how they relate to one another."
  };
  public static readonly ToDoItem ToDoItem3 = new ToDoItem
  {
    Title = "Run and Review Tests",
    Description = "Make sure all the tests run and review what they are doing."
  };


  public static readonly Project TestProject2 = new Project("Test Project 2", PriorityStatus.Backlog);
  public static readonly ToDoItem ToDoItem4 = new ToDoItem
  {
    Title = "Get Sample Working",
    Description = "Try to get the sample to build."
  };
  public static readonly ToDoItem ToDoItem5 = new ToDoItem
  {
    Title = "Review Solution",
    Description = "Review the different projects in the solution and how they relate to one another."
  };
  public static readonly ToDoItem ToDoItem6 = new ToDoItem
  {
    Title = "Run and Review Tests",
    Description = "Make sure all the tests run and review what they are doing."
  };

  public static void Initialize(IServiceProvider serviceProvider)
  {
    using (var dbContext = new AppDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null))
    {
      // Look for any TODO items.
      if (dbContext.ToDoItems.Any())
      {
        return;   // DB has been seeded
      }

      PopulateTestData(dbContext);


    }
  }
  public static void PopulateTestData(AppDbContext dbContext)
  {
    foreach (var item in dbContext.Projects)
    {
      dbContext.Remove(item);
    }
    foreach (var item in dbContext.ToDoItems)
    {
      dbContext.Remove(item);
    }
    dbContext.SaveChanges();

    TestProject1.AddItem(ToDoItem1);
    TestProject1.AddItem(ToDoItem2);
    TestProject1.AddItem(ToDoItem3);
    dbContext.Projects.Add(TestProject1);

    TestProject2.AddItem(ToDoItem4);
    TestProject2.AddItem(ToDoItem5);
    TestProject2.AddItem(ToDoItem6);
    dbContext.Projects.Add(TestProject2);

    dbContext.SaveChanges();
  }
}
