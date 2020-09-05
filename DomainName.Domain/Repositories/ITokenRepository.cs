using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
// user defined
using DomainName.Domain.Entities;

namespace DomainName.Domain.Repositories
{
    public interface ITokenRepository
    {

        Task<dynamic> CheckTokenAsync(string token);

        Task<ApplicationToken> AddTokenAsync(ApplicationToken tokenEntity);

    }
}
