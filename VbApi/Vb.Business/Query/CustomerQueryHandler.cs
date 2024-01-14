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

public class CustomerQueryHandler :
    IRequestHandler<GetAllCustomerQuery, ApiResponse<List<CustomerResponse>>>,
    IRequestHandler<GetCustomerByIdQuery, ApiResponse<CustomerResponse>>,
    IRequestHandler<GetCustomerByParameterQuery, ApiResponse<List<CustomerResponse>>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public CustomerQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetAllCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Customer>()
            .Include(x => x.Accounts)
            .Include(x => x.Contacts)
            .Include(x => x.Addresses).ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<Customer>, List<CustomerResponse>>(list);
         return new ApiResponse<List<CustomerResponse>>(mappedList);
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(GetCustomerByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<Customer>()
            .Include(x => x.Accounts)
            .Include(x => x.Contacts)
            .Include(x => x.Addresses)
            .FirstOrDefaultAsync(x => x.CustomerNumber == request.Id, cancellationToken);

        if (entity == null)
        {
            return new ApiResponse<CustomerResponse>("Record not found");
        }
        
        var mapped = mapper.Map<Customer, CustomerResponse>(entity);
        return new ApiResponse<CustomerResponse>(mapped);
    }

    public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetCustomerByParameterQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Customer>(true);
        if (string.IsNullOrEmpty(request.FirstName))
            
            predicate.And(x => x.FirstName.ToUpper().Contains(request.FirstName.ToUpper()));
        if (string.IsNullOrEmpty(request.LastName))
            predicate.And(x => x.LastName.ToUpper().Contains(request.LastName.ToUpper()));
        
        if (string.IsNullOrEmpty(request.IdentityNumber))
            predicate.And(x => x.IdentityNumber.ToUpper().Contains(request.IdentityNumber.ToUpper()));
        
        var list =  await dbContext.Set<Customer>()
            .Include(x => x.Accounts)
            .Include(x => x.Contacts)
            .Include(x => x.Addresses)
            .Where(predicate).ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<Customer>, List<CustomerResponse>>(list);
        return new ApiResponse<List<CustomerResponse>>(mappedList);
    }
}