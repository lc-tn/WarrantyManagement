using Microsoft.AspNetCore.Authorization;

namespace WarrantyManagement.Authorization
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission) : base(permission)
        {
        }
    }
}
