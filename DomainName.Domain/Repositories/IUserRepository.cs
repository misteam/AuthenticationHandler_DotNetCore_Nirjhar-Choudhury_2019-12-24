using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.AuthenticationTypes;
// user defined
using DomainName.Domain.Entities;
using DomainName.Domain.Responses;

namespace DomainName.Domain.Repositories
{
    public interface IUserRepository
    {

        Task<IEnumerable<User>> GetUsersAsync();

        Task<UserAuthenticationResponse> CheckUsernameAndPassAgainstRepositoryAsync(string username, string password);

        Task<ApplicationToken> AddSessionIdRecordAsync(ApplicationToken tokenObject);

    }
}
