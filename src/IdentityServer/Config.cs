using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        { 
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource()
            {
                Name = "verification",
                UserClaims = new List<string> 
                { 
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified
                }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        { 
            new ApiScope("api1", "MyAPI") 
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        { 
        };

    public static IEnumerable<Client> GetClients(IConfiguration configuration)
    {
        return new List<Client>
        {
            // machine-to-machine client (from quickstart 1)
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "api1" }
            },
            // interactive ASP.NET Core Web App
            new Client
            {
                ClientId = "web",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                // where to redirect after login
                // IDSD - Changed to allow property value to come from appsettings (and be overriden by docker-componse environment variables
                RedirectUris = { $"{configuration["Authentication:Clients:Web:Uri"]}/signin-oidc" },

                // where to redirect after logout,
                // IDSD - Changed to allow property value to come from appsettings (and be overriden by docker-componse environment variables
                PostLogoutRedirectUris = { $"{configuration["Authentication:Clients:Web:Uri"]}/signout-callback-oidc" },

                AllowOfflineAccess = true,

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1"
                }
            },
            // JavaScript Client
            new Client
            {
                ClientId = "js",
                ClientName = "JavaScript Client",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,

                RedirectUris =           { $"{configuration["Authentication:Clients:Javascript:Uri"]}/callback.html" },
                PostLogoutRedirectUris = { $"{configuration["Authentication:Clients:Javascript:Uri"]}/index.html" },
                AllowedCorsOrigins =     { $"{configuration["Authentication:Clients:Javascript:Uri"]}" },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1"
                }
            }
        };
    }
}