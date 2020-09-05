// System
using System.Threading.Tasks;
using DomainName.Domain.Entities;
using System.Collections.Generic;


namespace DomainName.Domain.Services
{
    public interface ICustomAuthenticationManager
    {

        Task<ApplicationToken> AuthenticateAsync(string username, string password);

        Task<ApplicationToken> CheckTokenAsync(string token);

    }
}