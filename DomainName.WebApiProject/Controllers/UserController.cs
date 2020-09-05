using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationHandler_DotNetCore_Nirjhar_Choudhury_2019_12_24.Models;
using DomainName.Domain.Services;
// microsoft
using Microsoft.AspNetCore.Authorization;
// microsoft
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationHandler_DotNetCore_Nirjhar_Choudhury_2019_12_24.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly 
            IJwtAuthenticationManager _jwtAuthenticationManager;

        public UserController(
            IJwtAuthenticationManager jwtAuthManager)
        {
            _jwtAuthenticationManager = jwtAuthManager;
        }



        // GET: api/name
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // GET: api/name/{id}
        //[Authorize(AuthenticationSchemes = "Cookies")] // default when commented
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/user/auth?
        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate(
            [FromBody] UserCredentials userCredentials)
        {
            var token = _jwtAuthenticationManager.Authenticate(
                userCredentials.Username, userCredentials.Password);

            if(token == null)
            {
                return Unauthorized();// returns 401 Unauthorized
            }

            return Ok(token);
        }



    }
}
