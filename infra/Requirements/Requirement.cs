
using Microsoft.AspNetCore.Authorization;

namespace auth_infra.Requirements
{
    public class Requirement : IAuthorizationRequirement
    {
        public string RequiredPermission { get; }
        public Requirement(string requiredPermission)
        {
            RequiredPermission = requiredPermission;
        }
    }
}