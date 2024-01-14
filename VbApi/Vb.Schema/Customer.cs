using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Vb.Base.Schema;

namespace Vb.Schema;

public class CustomerRequest : BaseRequest
{
    [JsonIgnore] 
    public int CustomerNumber { get; set; }
    public string IdentityNumber { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }

    public virtual List<AddressRequest> Addresses { get; set; }
    public virtual List<ContactRequest> Contacts { get; set; }
    public virtual List<AccountRequest> Accounts { get; set; }
}

public class CustomerResponse : BaseResponse
{
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CustomerNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime LastActivityDate { get; set; }

    public string CustomerName
    {
        get { return FirstName + " " + LastName; }
    }

    public virtual List<AddressResponse> Addresses { get; set; }
    public virtual List<ContactResponse> Contacts { get; set; }
    public virtual List<AccountResponse> Accounts { get; set; }
}