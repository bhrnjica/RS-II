using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentitySrv
{
    public static class IdentityConfiguration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };
        }

        /// <summary>
        /// Create available scopes (roles)
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApis()
        {
            var admin = new ApiResource("adminScope", new string[] { "role", "admin" });
            var user = new ApiResource("userScope", new string[] { "role", "user" });
            var lst = new List<ApiResource>();
            lst.Add(admin);
            lst.Add(user) ;
            return lst;
        }

        /// <summary>
        /// Creatio of available clients (users)
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            //firt clinet is administartor
            var admin = new Client
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
                AllowedScopes = { "adminScope", "userScope" }
            };


            //second client is user
            var user = new Client
            {
                ClientId = "user",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                
                // scopes that client has access to
                AllowedScopes = { "userScope" }
            };

            //create list of clients
            var lst = new List<Client>();
            lst.Add(admin);
            lst.Add(user);
            //
            return lst;
        }
    }
}