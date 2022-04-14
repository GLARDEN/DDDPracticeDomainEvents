using Ardalis.HttpClientTestExtensions;
using DDDPracticeDomainEvents.Web;
using DDDPracticeDomainEvents.Web.Endpoints.ProjectEndpoints;
using Xunit;

namespace DDDPracticeDomainEvents.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class ProjectList : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly HttpClient _client;

  public ProjectList(CustomWebApplicationFactory<WebMarker> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task ReturnsTwoProjects()
  {
    var result = await _client.GetAndDeserialize<ProjectListResponse>("/Projects");

    Assert.Equal(2, result.Projects.Count());
    Assert.Contains(result.Projects, i => i.Name == SeedData.TestProject1.Name);
    Assert.Contains(result.Projects, i => i.Name == SeedData.TestProject2.Name);
  }
}
