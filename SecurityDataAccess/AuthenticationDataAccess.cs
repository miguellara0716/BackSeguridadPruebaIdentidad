using Microsoft.Extensions.Configuration;
using SecurityBusinessEntities.Wrappers;
using SecurityDataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDataAccess
{
    public class AuthenticationDataAccess : IAuthenticationDataAccess
    {
        private readonly IConfiguration _configuration;
        public AuthenticationDataAccess (IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        public async Task<ResultLogin_Wrapper> LoginDataAccess(Login_Wrapper data)
        {
            ResultLogin_Wrapper result = new ResultLogin_Wrapper();
            result.IsSuccess = false;
            var connectionString = _configuration.GetConnectionString("develop");

            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("No connection string in config.json");

            await using (var con = new SqlConnection(connectionString))
            {
                await using (var cmd = new SqlCommand("pa_VerifyCredentials", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", data.user);
                    cmd.Parameters.AddWithValue("@Password", data.password);
                    con.Open();

                    var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                    if (reader.Read())
                    {
                        result.IsSuccess = true;
                    }
                    return result;
                }
            }
        } 
    }
}
