using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainName.Domain.Services
{
    public interface IJwtAuthenticationManager
    {

        public string Authenticate(string username, string password);

    }
}
