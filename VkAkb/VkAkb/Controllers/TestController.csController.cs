using Microsoft.AspNetCore.Mvc;

namespace VkAkb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{

    public TestController()
    {
        
    }
    // GET: api/TestController.cs
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET: api/TestController.cs/5
    [HttpGet("{id}", Name = "Get")]
    public string Get(int id)
    {
        return "value";
    }

    // POST: api/TestController.cs
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT: api/TestController.cs/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE: api/TestController.cs/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}