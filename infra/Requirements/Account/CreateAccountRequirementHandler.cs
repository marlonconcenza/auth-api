
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace auth_infra.Requirements.Account
{
    public class CreateAccountRequirementHandler : AuthorizationHandler<CreateAccountRequirement>
    {
        private const string AdministratorRoleName = "Admin";
        private AuthorizationHandlerContext _context;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateAccountRequirement requirement)
        {
            _context = context;

            var isAdministrator = IsAdministrator();
            var canCreateAccount = HasRequirements(requirement);

            if (isAdministrator && canCreateAccount)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        private bool IsAdministrator() => 
            GetClaim(ClaimTypes.Role, AdministratorRoleName);

        private bool HasRequirements(CreateAccountRequirement requirement) =>
            GetClaim("permissions", requirement.RequiredPermission);

        private bool GetClaim(string type, string value) => _context.User.HasClaim(type, value);
    }
}