using AutoMapper;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Business.Cqrs;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Query;

public class MoneyTransferTransactionQueryHandler :
    IRequestHandler<GetMoneyTransferTransactionByReferenceNumberQuery, ApiResponse<List<AccountTransactionResponse>>>,
    IRequestHandler<GetMoneyTransferTransactionByParameterQuery, ApiResponse<List<AccountTransactionResponse>>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public MoneyTransferTransactionQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetMoneyTransferTransactionByReferenceNumberQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<AccountTransaction>()
            .Include(x => x.Account).ThenInclude(x=> x.Customer).ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<AccountTransaction>, List<AccountTransactionResponse>>(list);
         return new ApiResponse<List<AccountTransactionResponse>>(mappedList);
    }


    public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetMoneyTransferTransactionByParameterQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<AccountTransaction>(true);
        
        if (string.IsNullOrEmpty(request.Description))
            predicate.And(x => x.Description.ToUpper().Contains(request.Description.ToUpper()));
        
        if (string.IsNullOrEmpty(request.TransferType))
            predicate.And(x => x.TransferType.ToUpper() == request.TransferType.ToUpper());

        if (string.IsNullOrEmpty(request.ReferenceNumber))
            predicate.And(x => x.ReferenceNumber.ToUpper() == request.ReferenceNumber.ToUpper());
        
        if (request.AccountNumber > 0)
            predicate.And(x => x.AccountId == request.AccountNumber);
        
        if (request.BeginAmount > 0)
            predicate.And(x => x.CreditAmount >= request.BeginAmount || x.DebitAmount >= request.BeginAmount);
        
        if (request.EndAmount > 0)
            predicate.And(x => x.CreditAmount <= request.EndAmount || x.DebitAmount <= request.EndAmount);
        
        
        var list =  await dbContext.Set<AccountTransaction>()
            .Include(x => x.Account).ThenInclude(x=> x.Customer)
            .Where(predicate).ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<AccountTransaction>, List<AccountTransactionResponse>>(list);
        return new ApiResponse<List<AccountTransactionResponse>>(mappedList);
    }
}