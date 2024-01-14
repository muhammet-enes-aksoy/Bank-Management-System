using AutoMapper;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Business.Cqrs;
using Vb.Business.Service;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Command;

public class CustomerCommandHandler :
    IRequestHandler<CreateCustomerCommand, ApiResponse<CustomerResponse>>,
    IRequestHandler<UpdateCustomerCommand,ApiResponse>,
    IRequestHandler<DeleteCustomerCommand,ApiResponse>

{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;
    private readonly INotificationService notificationService;

    public CustomerCommandHandler(VbDbContext dbContext,IMapper mapper,INotificationService notificationService)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.notificationService = notificationService;
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var checkIdentity = await dbContext.Set<Customer>().Where(x => x.IdentityNumber == request.Model.IdentityNumber)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkIdentity != null)
        {
            return new ApiResponse<CustomerResponse>($"{request.Model.IdentityNumber} is used by another customer.");
        }
        
        var entity = mapper.Map<CustomerRequest, Customer>(request.Model);
        entity.CustomerNumber = new Random().Next(1000000, 9999999);

        if (entity.Accounts.Any())
        {
            entity.Accounts.ForEach(x =>
            {
                x.AccountNumber = new Random().Next(10000000, 99999999);
            });
        }
        
        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);


        if (entity.Contacts.Any())
        {
            var email = entity.Contacts.FirstOrDefault(x => x.IsDefault && x.ContactType == "Email");
            if (email != null)
            {
                BackgroundJob.Schedule(() => notificationService.SendEmail("Welcome " + entity.FirstName ,email.Information,"Welcome on board!"), TimeSpan.FromSeconds(50));
            }
        }
      

        var mapped = mapper.Map<Customer, CustomerResponse>(entityResult.Entity);
        return new ApiResponse<CustomerResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Customer>().Where(x => x.CustomerNumber == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        
        fromdb.FirstName = request.Model.FirstName;
        fromdb.LastName = request.Model.LastName;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Customer>().Where(x => x.CustomerNumber == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        
        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}