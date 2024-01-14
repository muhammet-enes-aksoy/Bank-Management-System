using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Cqrs;

public record CreateMoneyTransferTransactionCommand(MoneyTransferTransactionRequest Model) : IRequest<ApiResponse<MoneyTransferTransactionResponse>>;

public record CreateEftTransactionCommand(EftTransactionRequest Model) : IRequest<ApiResponse<MoneyTransferTransactionResponse>>;


public record GetMoneyTransferTransactionByReferenceNumberQuery(string ReferenceNumber) : IRequest<ApiResponse<List<AccountTransactionResponse>>>;
public record GetMoneyTransferTransactionByParameterQuery(string ReferenceNumber,string Description,int? AccountNumber,decimal? BeginAmount, decimal? EndAmount,string TransferType) : IRequest<ApiResponse<List<AccountTransactionResponse>>>;