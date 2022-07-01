using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityBusiness.Interface;
using SecurityBusinessEntities.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationBusiness _business;
        public AuthenticationController(IAuthenticationBusiness Business)
        {
            _business = Business;
        }

        [HttpGet]
        [Route("Login")]
        public async Task<ActionResult> Login()
        {
            string authHeader = HttpContext.Request.Headers["Authorization"];
            if (String.IsNullOrEmpty(authHeader))
            {
                return Unauthorized();
            }
            ResultLogin_Wrapper Result = await _business.Login(authHeader);
            return Ok(Result);
        } 

    }
}
