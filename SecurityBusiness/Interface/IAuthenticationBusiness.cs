using SecurityBusinessEntities.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityBusiness.Interface
{
    public interface IAuthenticationBusiness
    {
        Task<ResultLogin_Wrapper> Login(string Credentials);
    }
}
