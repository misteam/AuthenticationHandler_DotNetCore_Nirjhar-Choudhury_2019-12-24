using DomainName.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// user defined
using Data.AuthenticationTypes;
using DomainName.Domain.Mappers;
using DomainName.Domain.Entities;
using DomainName.Domain.Responses;
using DomainName.Domain.Entities.Structs;

namespace DomainName.Domain.Services
{
    public class CustomAuthenticationManager : ICustomAuthenticationManager
    {

        private readonly IUserRepository _userRepository;
        private readonly IUserMapper _userMapper;
        private readonly ITokenRepository _tokenRepository;

        public CustomAuthenticationManager(
            IUserRepository userRepository,
            IUserMapper userMapper,
            ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _userMapper = userMapper;
            _tokenRepository = tokenRepository;
        }

        public async Task<ApplicationToken> AuthenticateAsync(string username, string password)
        {
            // check username & password
            UserAuthenticationResponse authenticationResponse = 
                await _userRepository.CheckUsernameAndPassAgainstRepositoryAsync(
                    username, password);


            if(authenticationResponse.Authenticated == true)
            {
                var sessionGuid = Guid.NewGuid().ToString();
                authenticationResponse.Token = sessionGuid;
                

                // Map UserAuthenticationResponse to ApplicationToken entitiy
                var applicationToken = _userMapper.Map(authenticationResponse);
                applicationToken.TokenType = TokenType.SessionGuid;

                if(applicationToken == 
                    await _userRepository.AddSessionIdRecordAsync(applicationToken))
                {
                    return applicationToken;
                }
            }
            
            return null;
            
        }

        public async Task<ApplicationToken> CheckTokenAsync(string token)
        {
            if(token != null)
            {
                var applicationToken = await _tokenRepository.CheckTokenAsync(token);
                return applicationToken;
            }

            return null;
        }
    }
}
