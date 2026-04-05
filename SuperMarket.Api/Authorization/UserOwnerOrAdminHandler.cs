using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SuperMarketManagementSystemApi.Authorization
{
    public class UserOwnerOrAdminHandler : AuthorizationHandler<UserOwnerOrAdminRequirments, int>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserOwnerOrAdminRequirments requirement, int userID)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userId, out int authenticatedstudentId) && authenticatedstudentId == userID)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

}
