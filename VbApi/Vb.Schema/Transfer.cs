using Vb.Base.Schema;

namespace Vb.Schema;

public class MoneyTransferTransactionRequest : BaseRequest
{
    public int FromAccountNumber { get; set; }
    public int ToAccountNumber { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
}

public class MoneyTransferTransactionResponse : BaseResponse
{
    public string ReferenceNumber { get; set; }
    public DateTime TransactionDate { get; set; }
}