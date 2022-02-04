namespace BlogSystem.Web.Infrastructure.Helpers
{
    using System.Security.Principal;
    using Data;

    public static class PrincipalExtensions
    {
        public static bool IsLoggedIn(this IPrincipal principal)
        {
            return principal.Identity.IsAuthenticated;
        }

        public static bool IsAdmin(this IPrincipal principal)
        {
            return principal.IsInRole(DataConstants.AdministratorRoleName);
        }
    }
}