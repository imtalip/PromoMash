namespace IdentityServerData.FakeData
{
    using IdentityServer4.Models;


    public class Scopes
    {
        public const string Api1 = "api1";
        public const string Api2 = "api2";
    }

    public class FakeData
    {
        public static List<IdentityResource> IdentityResources =
           new List<IdentityResource>
           {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile(),
           };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[] {
                new ApiScope(Scopes.Api1, "My API1"),
                new ApiScope(Scopes.Api2, "My API2")
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientId = "client1",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret1".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { Scopes.Api1 }
                },
                new Client
                {
                    ClientId = "client2",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret2".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { Scopes.Api2 }
                },
                new Client
                {
                    ClientId = "admin",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { Scopes.Api1, Scopes.Api2 }
                },
                new Client
                {
                    ClientId = "clientapp1",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    AllowOfflineAccess = true,
                    AllowedScopes = { Scopes.Api1, "openid", "profile" },

                    RedirectUris = new[]
                    {
                        $"http://localhost:4200/login-completed"
                    },
                    PostLogoutRedirectUris = new[]
                    {
                        $"http://localhost:4200/logout-completed"
                    }
                },
                new Client
                {
                    ClientId = "clientapp2",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    AllowOfflineAccess = true,
                    AllowedScopes = { Scopes.Api2, "openid", "profile" },

                    RedirectUris = new[]
                    {
                        $"http://localhost:4200/login-completed"
                    },
                    PostLogoutRedirectUris = new[]
                    {
                        $"http://localhost:4200/logout-completed"
                    }
                }
            };
    }
}
