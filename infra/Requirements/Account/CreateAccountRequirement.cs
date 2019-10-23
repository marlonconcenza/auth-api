
using Microsoft.AspNetCore.Authorization;

namespace auth_infra.Requirements.Account
{
    public class CreateAccountRequirement : IAuthorizationRequirement
    {
        public string RequiredPermission { get; }
        public CreateAccountRequirement(string requiredPermission)
        {
            RequiredPermission = requiredPermission;
        }
    }
}