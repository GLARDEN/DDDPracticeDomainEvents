using Ardalis.HttpClientTestExtensions;
using DDDPracticeDomainEvents.Web;
using DDDPracticeDomainEvents.Web.ApiModels;
using Xunit;

namespace DDDPracticeDomainEvents.FunctionalTests.ControllerApis;

[Collection("Sequential")]
public class ProjectCreate : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly HttpClient _client;

  public ProjectCreate(CustomWebApplicationFactory<WebMarker> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task ReturnsTwoProjects()
  {
    var result = await _client.GetAndDeserialize<IEnumerable<ProjectDTO>>("/api/projects");

    Assert.Equal(2, result.Count());
    Assert.Contains(result, i => i.Name == SeedData.TestProject1.Name);
    Assert.Contains(result, i => i.Name == SeedData.TestProject2.Name);
  }
}
