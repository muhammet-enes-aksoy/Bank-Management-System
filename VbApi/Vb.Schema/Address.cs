using System.Text.Json.Serialization;
using Vb.Base.Schema;

namespace Vb.Schema;

public class AddressRequest : BaseRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    
    public int CustomerId { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string PostalCode { get; set; }
    public bool IsDefault { get; set; }

}

public class AddressResponse : BaseResponse
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string PostalCode { get; set; }
    public bool IsDefault { get; set; }
}