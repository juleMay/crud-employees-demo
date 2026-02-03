using Microsoft.AspNetCore.Identity;

namespace WebApi.Domain.Entities.Authentication;

public class ApplicationUser : IdentityUser
{
    public string FirebaseUserId { get; set; }
    public string Name { get; set; }
}