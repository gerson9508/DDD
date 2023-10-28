using IdentityModel;
using IdentityServer4.Models;
using System.Security.Cryptography.X509Certificates;

namespace _360.IdentityServer.Infraestructure.Repositories.Base
{
   public static class Config
   {
      public static X509Certificate2 GetCert(string path, string password) => 
         new(File.ReadAllBytes(path), password);

      public static IEnumerable<IdentityResource> GetIdentityResources()
      {
         return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
      }

      public static IEnumerable<ApiScope> ApiScopes =>
         new[] { new ApiScope("http://localhost:81/"), };

      public static IEnumerable<ApiResource> GetApis()
      {
         return new List<ApiResource>
           {
                  new ApiResource("http://localhost:81/")
                  {
                     Name = "API",
                     DisplayName = "_360",
                     Description = "Checking endpoints",
                     ApiSecrets =
                     {
                        new Secret("secret_for_the_api_360".Sha256()),
                        new Secret("secret_for_the_api_360_2".Sha256())
                     },
                     Enabled = true,
                     UserClaims =
                     {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        //JwtClaimTypes.Email
                     }
                  },

            };
      }

      public static IEnumerable<Client> GetClients()
      {
         return new List<Client>
         {
            // OpenID Connect hybrid flow client (MVC)
            //new Client
            //{
            //    ClientId = "mvc",
            //    ClientName = "MVC Client",
            //    AllowedGrantTypes = GrantTypes.Code,

            //    ClientSecrets =
            //    {
            //        new Secret("secret".Sha256())
            //    },

            //    RedirectUris           = { "https://localhost:7088/signin-oidc" },
            //    PostLogoutRedirectUris = { "https://localhost:7088/signout-callback-oidc" },

            //    AllowedScopes =
            //    {
            //        IdentityServerConstants.StandardScopes.OpenId,
            //        IdentityServerConstants.StandardScopes.Profile,
            //        "api1"
            //    },

            //    AllowOfflineAccess = true,
            //    RequirePkce = true,
            //},
             // resource owner password grant client
            new Client
            {
               ClientId = "clientid_for_the_360",
               RequirePkce = true,
               Enabled = true,
               AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
               ClientSecrets =
               {
                  new Secret("secret_for_the_360".Sha256()),
                  new Secret("secret_for_the_360_2".Sha256()),
               },
               AccessTokenType = AccessTokenType.Jwt,
               //RefreshTokenUsage = TokenUsage.ReUse,
               //UpdateAccessTokenClaimsOnRefresh = true,
               AllowedScopes =
               {
                  "http://localhost:81/"
               },
            }
         };
      }
   }
}
