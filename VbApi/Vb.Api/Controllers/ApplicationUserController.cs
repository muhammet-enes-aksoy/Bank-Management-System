using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vb.Base.Response;
using Vb.Business.Cqrs;
using Vb.Schema;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicationUsersController : ControllerBase
{
    private readonly IMediator mediator;

    public ApplicationUsersController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    
    [HttpGet("MyProfile")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<ApplicationUserResponse>> MyProfile()
    {
        string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new GetApplicationUserByIdQuery(int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<ApplicationUserResponse>>> Get()
    {
        var operation = new GetAllApplicationUserQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<ApplicationUserResponse>> Get(int id)
    {
        var operation = new GetApplicationUserByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("ByParameters")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<ApplicationUserResponse>>> GetByParameter(
        [FromQuery] string? FirstName,
        [FromQuery] string? LastName,
        [FromQuery] string? UserName)
    {
        var operation = new GetApplicationUserByParameterQuery(FirstName,LastName,UserName);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<ApplicationUserResponse>> Post([FromBody] ApplicationUserRequest ApplicationUser)
    {
        var operation = new CreateApplicationUserCommand(ApplicationUser);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] ApplicationUserRequest ApplicationUser)
    {
        var operation = new UpdateApplicationUserCommand(id,ApplicationUser );
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteApplicationUserCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}