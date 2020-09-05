using Data.AuthenticationTypes;
using DomainName.Domain.Entities;
using DomainName.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainName.Domain.Mappers
{
    public class UserMapper : IUserMapper
    {
        public ApplicationToken Map(UserAuthenticationResponse authResponse)
        {
            return new ApplicationToken(
                authResponse.Token,
                authResponse.User.AuthenticationDurationInMilliseconds);
        }

        //public ApplicationToken Map(UserAuthenticationResponse authResponse)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
