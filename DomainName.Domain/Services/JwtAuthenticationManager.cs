using Microsoft.IdentityModel.Tokens;

using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainName.Domain.Services
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {

        IDictionary<string, string> users = new Dictionary<string, string>
        {
            {"test1", "password1" },
            {"test2", "password2" }
        };

        private readonly string tokenKeyString;

        public JwtAuthenticationManager(string tokenKey)
        {
            this.tokenKeyString = tokenKey;
        }


        public string Authenticate(string username, string password)
        {

            #region Check UserRepository for user authentication
            // check username and password against the data store
            if(!users.Any(u => u.Key == username && u.Value == password))
            {

                // if nothing matches, return null
                return null;
            }
            #endregion UserRepository


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKeyBytes = Encoding.ASCII.GetBytes(tokenKeyString);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
