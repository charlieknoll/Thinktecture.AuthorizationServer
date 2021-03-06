﻿using System.Web.Http;
using Thinktecture.IdentityModel;
using Thinktecture.IdentityModel.Tokens.Http;
using System.Linq;
using Thinktecture.IdentityModel.Tokens;

namespace Thinktecture.Samples
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableSystemDiagnosticsTracing();
            config.MessageHandlers.Add(new AuthenticationHandler(CreateAuthenticationConfiguration()));
        }

        private static AuthenticationConfiguration CreateAuthenticationConfiguration()
        {
            var authentication = new AuthenticationConfiguration
            {
                RequireSsl = false,
            };

            authentication.AddJsonWebToken(
                issuer: Constants.AuthzSrv.IssuerName,
                audience: Constants.Audience,
                signingKey: Constants.AuthzSrv.SigningKey,
                claimMappings: ClaimMappings.None);

            return authentication;
        }
    }
}
