using Microsoft.AspNetCore.Identity;

namespace Dev.Plugin.Auth.Core.Domain;

public class DevUser : IdentityUser
{
    public string? FullName { get; set; }
}
