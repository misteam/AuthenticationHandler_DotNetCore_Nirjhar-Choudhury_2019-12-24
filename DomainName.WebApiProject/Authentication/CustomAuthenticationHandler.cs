using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Security.Claims;
// microsoft
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
// user defined
using DomainName.Domain.Services;

namespace AuthenticationHandler_DotNetCore_Nirjhar_Choudhury_2019_12_24.Authentication
{
    public class CustomAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {

        #region Construction & DependencyInjection

        private readonly ICustomAuthenticationManager _customAuthenticationManager;

        public CustomAuthenticationHandler(
            //AuthenticationOptions
            IOptionsMonitor<BasicAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            //local readonly
            ICustomAuthenticationManager customAuthenticationManager)
                : base(options, logger, encoder, clock)
        {
            // below can also be 'this.' removing the '_'
            _customAuthenticationManager = customAuthenticationManager; 
        }

        #endregion Construction & DependencyInjection

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.NoResult();
            }

            if(!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            // return the token
            string token = authorizationHeader.Substring("bearer".Length).Trim();

            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            try
            {
                return await validateToken(token);
            }
            catch(Exception ex)
            {
                return AuthenticateResult.Fail($"{ex.Message}");
            }

        }// end HandleAuthenticateAsync()


        private async Task<AuthenticateResult> validateToken(string token)
        {
            var validatedToken 
                = await _customAuthenticationManager.CheckTokenAsync(token);
            if(validatedToken.Token == null)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, validatedToken.User.UserId),
                new Claim("Role", validatedToken.User.Role)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);

        }


        
    }
}
