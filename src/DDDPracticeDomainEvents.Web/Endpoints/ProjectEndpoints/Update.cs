using Ardalis.ApiEndpoints;

using DDDPracticeDomainEvents.Core.ProjectAggregate;
using DDDPracticeDomainEvents.Core.ProjectAggregate.Exceptions;
using DDDPracticeDomainEvents.SharedKernel.Interfaces;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace DDDPracticeDomainEvents.Web.Endpoints.ProjectEndpoints;

public class Update : EndpointBaseAsync
    .WithRequest<UpdateProjectRequest>
    .WithActionResult<UpdateProjectResponse>
{
  private readonly IRepository<Project> _repository;

  public Update(IRepository<Project> repository)
  {
    _repository = repository;
  }

  [HttpPut(UpdateProjectRequest.Route)]
  [SwaggerOperation(
      Summary = "Updates a Project",
      Description = "Updates a Project with a longer description",
      OperationId = "Projects.Update",
      Tags = new[] { "ProjectEndpoints" })
  ]
  public override async Task<ActionResult<UpdateProjectResponse>> HandleAsync(UpdateProjectRequest request,
      CancellationToken cancellationToken)
  {
    if (request.Name == null)
    {
      return BadRequest();
    }
    var existingProject = await _repository.GetByIdAsync(request.Id); // TODO: pass cancellation token

    if (existingProject == null)
    {
      return NotFound();
    }

    try
    {
      existingProject.UpdateName(request.Name);
    }
    catch(DuplicateProjectNameException ex)
    {
      this.ModelState.AddModelError("name", ex.Message);
      return BadRequest(ModelState);
    }
    

    await _repository.UpdateAsync(existingProject); // TODO: pass cancellation token

    var response = new UpdateProjectResponse(
        project: new ProjectRecord(existingProject.Id, existingProject.Name)
    );
    return Ok(response);
  }
}
