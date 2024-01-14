using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Vb.Base.Response;
using Vb.Business.Cqrs;
using Vb.Schema;
using VbApi.Filter;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransfersController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IMemoryCache memoryCache;
    private readonly IDistributedCache distributedCache;

    public TransfersController(IMediator mediator, IMemoryCache memoryCache,IDistributedCache distributedCache)
    {
        this.mediator = mediator;
        this.memoryCache = memoryCache;
        this.distributedCache = distributedCache;
    }

    [HttpPost("Transfer")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<MoneyTransferTransactionResponse>> Transfer(
        [FromBody] MoneyTransferTransactionRequest request)
    {
        var operation = new CreateMoneyTransferTransactionCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost("EFT")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<MoneyTransferTransactionResponse>> EFT([FromBody] EftTransactionRequest request)
    {
        var operation = new CreateEftTransactionCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }


    [HttpGet("ByReferenceNumberMemory/{ReferenceNumber}")]
    public async Task<ApiResponse<List<AccountTransactionResponse>>> ByReferenceNumberMemory(string ReferenceNumber)
    {
        var cacheResult =
            memoryCache.TryGetValue(ReferenceNumber, out ApiResponse<List<AccountTransactionResponse>> cacheData);
        if (cacheResult)
        {
            return cacheData;
        }
        
        var operation = new GetMoneyTransferTransactionByReferenceNumberQuery(ReferenceNumber);
        var result = await mediator.Send(operation);

        if (result.Response.Any())
        {
            var options = new MemoryCacheEntryOptions()
            {
                Priority = CacheItemPriority.High,
                AbsoluteExpiration = DateTime.Now.AddDays(1),
                SlidingExpiration = TimeSpan.FromHours(1)
            };
            memoryCache.Set(ReferenceNumber, result, options);
        }
        return result;
    }
    
    [HttpGet("ByReferenceNumberRedis/{ReferenceNumber}")]
    public async Task<ApiResponse<List<AccountTransactionResponse>>> ByReferenceNumberRedis(string ReferenceNumber)
    {
        var cacheResult = await  distributedCache.GetAsync(ReferenceNumber);
        if (cacheResult != null)
        {
            string json = Encoding.UTF8.GetString(cacheResult);
            var response = JsonConvert.DeserializeObject<List<AccountTransactionResponse>>(json);
            return new ApiResponse<List<AccountTransactionResponse>>(response);
        }
        
        var operation = new GetMoneyTransferTransactionByReferenceNumberQuery(ReferenceNumber);
        var result = await mediator.Send(operation);

        if (result.Response.Any())
        {
            string responseJson = JsonConvert.SerializeObject(result.Response);
            byte[] responseArr = Encoding.UTF8.GetBytes(responseJson);
            
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddDays(1),
                SlidingExpiration = TimeSpan.FromHours(1)
            };
           await  distributedCache.SetAsync(ReferenceNumber, responseArr, options);
        }
        return result;
    }
    
    [HttpDelete("ByReferenceNumber/{ReferenceNumber}/DeleteCache")]
    public async Task<ApiResponse> DeleteCache(string ReferenceNumber)
    {
        memoryCache.Remove(ReferenceNumber);
        distributedCache.Remove(ReferenceNumber);
        return new ApiResponse();
    }

    [HttpGet("ByParameters")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<AccountTransactionResponse>>> GetByParameter(
        [FromQuery] string? ReferenceNumber,
        [FromQuery] string? Description,
        [FromQuery] int? AccountNumber,
        [FromQuery] decimal? BeginAmount,
        [FromQuery] decimal? EndAmount,
        [FromQuery] string? TransferType)
    {
        var operation = new GetMoneyTransferTransactionByParameterQuery(ReferenceNumber, Description, AccountNumber,
            BeginAmount, EndAmount, TransferType);
        var result = await mediator.Send(operation);
        return result;
    }
}