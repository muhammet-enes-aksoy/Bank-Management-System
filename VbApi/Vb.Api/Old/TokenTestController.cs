using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VbApi.Controllers;


[Route("api/[controller]")]
[ApiController]
[NonController]
public class TokenTestController  : ControllerBase
{
    public TokenTestController( )
    {
    }
    
    [HttpGet("NoToken")]
    public string NoToken()
    {
        return "NoToken";
    }

    [Authorize]
    [HttpGet("Authorize")]
    public string Authorize()
    {
        return "Authorize";
    }


    [Authorize(Roles = "admin")]
    [HttpGet("Admin")]
    public string Admin()
    {
        return "Admin";
    }

    [Authorize(Roles = "viewer")]
    [HttpGet("Viewer")]
    public string Viewer()
    {
        return "Viewer";
    }


    [Authorize(Roles = "viewer,admin")]
    [HttpGet("ViewerAdmin")]
    public string ViewerAdmin()
    {
        return "ViewerAdmin";
    }
}