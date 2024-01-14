using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Business.Cqrs;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Command;

public class MoneyTransferTransactionCommandHandler :
    IRequestHandler<CreateMoneyTransferTransactionCommand, ApiResponse<MoneyTransferTransactionResponse>>,
    IRequestHandler<CreateEftTransactionCommand, ApiResponse<MoneyTransferTransactionResponse>>
{
    private readonly VbDbContext dbContext;

    public MoneyTransferTransactionCommandHandler(VbDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ApiResponse<MoneyTransferTransactionResponse>> Handle(
        CreateMoneyTransferTransactionCommand request, CancellationToken cancellationToken)
    {
        var fromAccountResponse = await GetAccount(request.Model.FromAccountNumber, cancellationToken);
        if (!fromAccountResponse.Success)
        {
            return new ApiResponse<MoneyTransferTransactionResponse>(fromAccountResponse.Message);
        }

        var toAccountResponse = await GetAccount(request.Model.ToAccountNumber, cancellationToken);
        if (!toAccountResponse.Success)
        {
            return new ApiResponse<MoneyTransferTransactionResponse>(toAccountResponse.Message);
        }

        Account fromAccount = fromAccountResponse.Response;
        Account toAccount = toAccountResponse.Response;

        if (fromAccount.Balance < request.Model.Amount)
        {
            return new ApiResponse<MoneyTransferTransactionResponse>($"Balance is not available.");
        }

        if (fromAccount.CurrencyType != toAccount.CurrencyType)
        {
            return new ApiResponse<MoneyTransferTransactionResponse>($"Account currencies must be same.");
        }

        string transactionType;
        if (fromAccount.CustomerId == toAccount.CustomerId)
            transactionType = "VIRMAN";
        else
            transactionType = "HAVALE";

        string refNo = DateTime.Now.Ticks.ToString();

        AccountTransaction fromTransaction = new AccountTransaction();
        fromTransaction.AccountId = fromAccount.AccountNumber;
        fromTransaction.CreditAmount = request.Model.Amount;
        fromTransaction.TransactionDate = DateTime.Now;
        fromTransaction.Description =
            $"{fromAccount.AccountNumber} no lu hesaptan {toAccount.AccountNumber} nolu hesaba " + transactionType +
            ". " + request.Model.Description;
        fromTransaction.DebitAmount = 0;
        fromTransaction.TransferType = transactionType;
        fromTransaction.ReferenceNumber = refNo;

        AccountTransaction toTransaction = new AccountTransaction();
        toTransaction.AccountId = toAccount.AccountNumber;
        toTransaction.DebitAmount = request.Model.Amount;
        toTransaction.TransactionDate = DateTime.Now;
        toTransaction.Description =
            $"{toAccount.AccountNumber} no lu hesaba {fromAccount.AccountNumber} nolu hesaptan gelen " +
            transactionType + ". " + request.Model.Description;
        toTransaction.CreditAmount = 0;
        toTransaction.TransferType = transactionType;
        toTransaction.ReferenceNumber = refNo;

        fromAccount.Balance -= request.Model.Amount;
        toAccount.Balance += request.Model.Amount;

        await dbContext.AddAsync(fromTransaction, cancellationToken);
        await dbContext.AddAsync(toTransaction, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = new MoneyTransferTransactionResponse
        {
            TransactionDate = DateTime.Now,
            ReferenceNumber = refNo
        };
        return new ApiResponse<MoneyTransferTransactionResponse>(response);
    }

    public async Task<ApiResponse<MoneyTransferTransactionResponse>> Handle(CreateEftTransactionCommand request,
        CancellationToken cancellationToken)
    {
        var fromAccountResponse = await GetAccount(request.Model.AccountId, cancellationToken);
        if (!fromAccountResponse.Success)
        {
            return new ApiResponse<MoneyTransferTransactionResponse>(fromAccountResponse.Message);
        }

        Account fromAccount = fromAccountResponse.Response;
        if (fromAccount.Balance < request.Model.Amount)
        {
            return new ApiResponse<MoneyTransferTransactionResponse>($"Balance is not available.");
        }

        string refNo = DateTime.Now.Ticks.ToString();

        AccountTransaction fromTransaction = new AccountTransaction();
        fromTransaction.AccountId = fromAccount.AccountNumber;
        fromTransaction.CreditAmount = request.Model.Amount;
        fromTransaction.TransactionDate = DateTime.Now;
        fromTransaction.Description =
            $"{fromAccount.AccountNumber} no lu hesaptan ${request.Model.ReceiverName} `nin  ${request.Model.ReceiverAccount} nolu hesabina EFT. " +
            request.Model.Description;
        fromTransaction.DebitAmount = 0;
        fromTransaction.TransferType = "EFT";
        fromTransaction.ReferenceNumber = refNo;

        EftTransaction toTransaction = new EftTransaction();
        toTransaction.AccountId = fromAccount.AccountNumber;
        toTransaction.Amount = request.Model.Amount;
        toTransaction.TransactionDate = DateTime.Now;
        toTransaction.Description = fromTransaction.Description;
        toTransaction.ReferenceNumber = refNo;
        toTransaction.ReceiverAccount = request.Model.ReceiverAccount;
        toTransaction.ReceiverName = request.Model.ReceiverName;
        toTransaction.ReceiverIban = request.Model.ReceiverIban;

        fromAccount.Balance -= request.Model.Amount;

        await dbContext.AddAsync(fromTransaction, cancellationToken);
        await dbContext.AddAsync(toTransaction, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = new MoneyTransferTransactionResponse
        {
            TransactionDate = DateTime.Now,
            ReferenceNumber = refNo
        };
        return new ApiResponse<MoneyTransferTransactionResponse>(response);
    }

    private async Task<ApiResponse<Account>> GetAccount(int AccountNumber, CancellationToken cancellationToken)
    {
        var account = await dbContext.Set<Account>().Include(x => x.Customer)
            .Where(x => x.AccountNumber == AccountNumber)
            .FirstOrDefaultAsync(cancellationToken);
        if (account == null)
        {
            return new ApiResponse<Account>($"{AccountNumber} not found.");
        }

        if (!account.IsActive)
        {
            return new ApiResponse<Account>($"{AccountNumber} not active.");
        }

        if (!account.Customer.IsActive)
        {
            return new ApiResponse<Account>(
                $"{account.Customer.CustomerNumber} customer not active for account ${AccountNumber}.");
        }

        return new ApiResponse<Account>(account);
    }
}