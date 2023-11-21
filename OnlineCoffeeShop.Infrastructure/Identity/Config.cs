using Duende.IdentityServer.Models;

namespace OnlineCoffeeShop.Infrastructure.Identity;
public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
      new[]
      {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResource
        {
          Name = "role",
          UserClaims = new List<string> {"role"}
        }
      };

    public static IEnumerable<ApiScope> ApiScopes =>
      new[]
      {
        new ApiScope("onlinecoffeeshop.read"),
        new ApiScope("onlinecoffeeshop.write"),
      };

    public static IEnumerable<ApiResource> ApiResources => new[]
    {
      new ApiResource("onlinecoffeeshop")
      {
        Scopes = new List<string> {"onlinecoffeeshop.read", "onlinecoffeeshop.write"},
        ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
        UserClaims = new List<string> {"role"}
      }
    };

    public static IEnumerable<Client> Clients =>
      new[]
      {
        // m2m client credentials flow client
        new Client
        {
          ClientId = "m2m.client",
          ClientName = "Client Credentials Client",

          AllowedGrantTypes = GrantTypes.ClientCredentials,
          ClientSecrets = {new Secret("197d7878-4767-44b6-94b0-0f9e79e81317".Sha256())},

          AllowedScopes = {"onlinecoffeeshop.read", "onlinecoffeeshop.write"}
        },

        // interactive client using code flow + pkce
        new Client
        {
          ClientId = "interactive",
          ClientSecrets = {new Secret("13c5ee5d-d0c7-47c1-ab52-3d74f3fa6dc6".Sha256())},

          AllowedGrantTypes = GrantTypes.Code,

          RedirectUris = {"https://localhost:5444/signin-oidc"},
          FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
          PostLogoutRedirectUris = {"https://localhost:5444/signout-callback-oidc"},

          AllowOfflineAccess = true,
          AllowedScopes = {"openid", "profile", "onlinecoffeeshop.read", "onlinecoffeeshop.write"},
          RequirePkce = true,
          RequireConsent = true,
          AllowPlainTextPkce = false
        },
      };
}
