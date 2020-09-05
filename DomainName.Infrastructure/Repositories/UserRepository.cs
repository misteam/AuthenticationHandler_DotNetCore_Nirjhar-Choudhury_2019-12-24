using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Data.AuthenticationTypes;
using DomainName.Domain.Entities;
// user defined
using DomainName.Domain.Repositories;
// other
using Dapper;
using System.Data;
using DomainName.Domain.Responses;

namespace DomainName.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly SqlConnection _sqlConnection;

        public UserRepository(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }

        public async Task<ApplicationToken> AddSessionIdRecordAsync(ApplicationToken tokenObject)
        {
            var response = await _sqlConnection.QueryFirstAsync(
                $"Insert into Auth.Sessions(DurationInMilliseconds, NotValidBefore, GeneratedAt, Token, TokenType, UserId)" +
                $"VALUES (@DurationInMilliseconds, @NotValidBefore, @GeneratedAt, @Token, @TokenType, @UserId)",
                new
                {
                    DurationInMilliseconds = tokenObject.DurationInMilliseconds,
                    NotValidBefore = tokenObject.NotValidBefore,
                    GeneratedAt  = tokenObject.GeneratedAt,
                    // could save the 'token' entity as its own type
                    Token = tokenObject.Token,
                    TokenType = tokenObject.TokenType,
                    UserId = tokenObject.User.UserId
                });

            return tokenObject; //testing only, check against what has been saved
            // > in production
        }

        public async Task<UserAuthenticationResponse> CheckUsernameAndPassAgainstRepositoryAsync(string username, string password)
        {

            var user = await _sqlConnection.QueryFirstAsync(
                $"select * from tblSystemAccounts" +
                $"where @Username = username" +
                $"  and @Password = passwordHash;",
                new
                {
                    Password = password,
                    Username = username
                },
                commandType: CommandType.Text);

            var response =  new UserAuthenticationResponse()
            {
                User = new User
                {
                    Id = username
                },
                Authenticated = true,
                
            };

            return response;
        }

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
