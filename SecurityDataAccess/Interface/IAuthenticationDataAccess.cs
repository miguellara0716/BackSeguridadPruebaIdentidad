using SecurityBusinessEntities.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDataAccess.Interface
{
    public interface IAuthenticationDataAccess
    {
        Task<ResultLogin_Wrapper> LoginDataAccess(Login_Wrapper data);
    }
}
