using Duende.IdentityServer.Models;
using IdentityModel;

namespace HexId.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new ApiScope(name: "HexIdExampleApi", displayName: "HexId Example API")
    ];

    public static IEnumerable<Client> Clients =>
    [
        new Client 
        {
            ClientId = "hexid_example_client",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { new Secret("hexid_example_secret".Sha256()) },
            RequireClientSecret = false,
            AllowedScopes = { "HexIdExampleApi" }
        }
    ];
}