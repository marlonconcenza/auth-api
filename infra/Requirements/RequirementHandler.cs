using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace auth_infra.Requirements
{
    public class RequirementHandler : AuthorizationHandler<Requirement>
    {
        //private const string AdministratorRoleName = "Admin";
        private AuthorizationHandlerContext _context;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Requirement requirement)
        {
            _context = context;

            //var isAdministrator = IsAdministrator();
            var canAction = HasRequirements(requirement);

            if (canAction)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        //private bool IsAdministrator() => GetClaim(ClaimTypes.Role, AdministratorRoleName);

        private bool HasRequirements(Requirement requirement) =>
            GetClaim("permissions", requirement.RequiredPermission);

        private bool GetClaim(string type, string value) => _context.User.HasClaim(type, value);
    }
}