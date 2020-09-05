using Data.AuthenticationTypes;
using DomainName.Domain.Entities;
using DomainName.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainName.Domain.Mappers
{
    public interface IUserMapper
    {

        public ApplicationToken Map(UserAuthenticationResponse authResponse);

    }
}
