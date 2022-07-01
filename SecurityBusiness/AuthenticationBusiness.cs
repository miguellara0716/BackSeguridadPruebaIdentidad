using SecurityBusiness.Interface;
using SecurityBusinessEntities.Wrappers;
using SecurityDataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityBusiness
{
    public class AuthenticationBusiness : IAuthenticationBusiness
    {
        private readonly IAuthenticationDataAccess _dataAccess;

        public AuthenticationBusiness (IAuthenticationDataAccess DataAccess)
        {
            _dataAccess = DataAccess;
        }

        public async  Task<ResultLogin_Wrapper> Login(string Credentials)
        {
            ResultLogin_Wrapper Result = new ResultLogin_Wrapper();
            Login_Wrapper Data = new Login_Wrapper();
            if (Credentials != null && Credentials.StartsWith("Basic"))
            {
                string encodedUsernamePassword = Credentials.Substring("Basic ".Length).Trim();
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
                int seperatorIndex = usernamePassword.IndexOf(':');
                string username = usernamePassword.Substring(0, seperatorIndex);
                string password = usernamePassword.Substring(seperatorIndex + 1);
                byte[] encodedByteUser = Encoding.ASCII.GetBytes(username);
                Data.user = Convert.ToBase64String(encodedByteUser);
                byte[] encodedBytePaswoord = Encoding.ASCII.GetBytes(password);
                Data.password = Convert.ToBase64String(encodedBytePaswoord);
            }
            else
            {
                Result.IsSuccess = false;

                return Result;
            }           
            Result = await _dataAccess.LoginDataAccess(Data);

            return Result;
        }
    }
}
