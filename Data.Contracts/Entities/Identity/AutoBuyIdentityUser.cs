using Microsoft.AspNetCore.Identity;

namespace Data.Contracts;

public class AutoBuyIdentityUser : IdentityUser
{
    public string Address { get; set; }
    
    public string City { get; set; }
    
    public string Country { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string PostalCode { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}