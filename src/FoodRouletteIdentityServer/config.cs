using IdentityServer4.Models;
using IdentityServer4.Services.InMemory;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;

namespace FoodRouletteIdentityServer
{
    public class Config
    {
        /// <summary>
        /// Scopes define the resources in your system that you want to protect
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Scope> GetScopes()
        {
            return new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,

                new Scope
                {
                    Name = "food",
                    Description = "Give me food!"
                }
            };
        }


        /// <summary>
        /// a client that can access above scope
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients(IConfigurationRoot configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientName = "Food Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    RequireConsent = false,

                    // where to redirect to after login
                    RedirectUris = { configuration["RedirectUri"] },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { configuration["PostLogoutRedirectUri"] },

                    AllowedScopes = new List<string>
                    {
                        StandardScopes.OpenId.Name,
                        StandardScopes.Profile.Name,
                        "food"
                    }
                }
            };
        }

        public static List<InMemoryUser> GetUsers(IConfigurationRoot configuration)
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "1",
                    Username = configuration["User"],
                    Password = configuration["Secret"],
                    Claims = new []
                    {
                        new Claim("name", "Ken Tor"),
                        new Claim("website", "http://familjelivellerflashback.se/")
                    }
                }
            };
        }
    }
}
