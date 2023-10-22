using System.Security.Claims;

namespace ElectronicsStore.BackendApi.Extendsions
{
    public static class IdentityExtendsion
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = ((ClaimsIdentity)claimsPrincipal.Identity)
                .Claims
                .SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return claim.Value;
        }
    }
}
