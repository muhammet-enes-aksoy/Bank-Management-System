using System.Text.Json.Serialization;
using Vb.Base.Schema;

namespace Vb.Schema;

public class ContactRequest : BaseRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    
    public int CustomerId { get; set; }
    public string ContactType { get; set; }
    public string Information { get; set; }
    public bool IsDefault { get; set; }
}


public class ContactResponse : BaseResponse
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string ContactType { get; set; }
    public string Information { get; set; }
    public bool IsDefault { get; set; }
}
