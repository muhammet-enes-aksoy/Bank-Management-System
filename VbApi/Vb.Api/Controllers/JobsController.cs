using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobsController : ControllerBase
{
    private readonly IMediator mediator;

    public JobsController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpGet("FireAndForget")]
    public string FireAndForget()
    {
        var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget!"));
        return jobId;
    }

    [HttpGet("Delayed")]
    public string Delayed()
    {
        var jobId = BackgroundJob.Schedule(() => Console.WriteLine("Delayed!"), TimeSpan.FromMinutes(7));
        return jobId;
    }

    [HttpGet("Recurring")]
    public string Recurring()
    {
        RecurringJob.AddOrUpdate("REC0004", () => Console.WriteLine("Recurring!"), "00 18 * * 1");
        return "REC0004";
    }

    [HttpGet("Continuations")]
    public string Continuations()
    {
        var jobId = BackgroundJob.Schedule(() => Console.WriteLine("Delayed!"), TimeSpan.FromMinutes(1));
        BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("Continuation!"));
        return jobId;
    }
}