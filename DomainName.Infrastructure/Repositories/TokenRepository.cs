using DomainName.Domain.Entities;
using DomainName.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
// other
using Dapper;



namespace DomainName.Infrastructure.Repositories
{
    public class TokenRepository : ITokenRepository
    {

        private readonly SqlConnection _sqlConnection;

        public TokenRepository(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }



        public Task<ApplicationToken> AddTokenAsync(ApplicationToken tokenEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> CheckTokenAsync(string token)
        {
            var matchingToken = await _sqlConnection.QueryAsync(
                $"select * from auth.Tokens" +
                $"where Token  = @Token",
                new
                {
                    Token = token
                },
                commandType: CommandType.Text);

            return  matchingToken;
        }

        
    }
}
