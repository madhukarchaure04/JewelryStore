using JewelryStore.API.Authorization;
using JewelryStore.API.Entities;
using JewelryStore.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace JewelryStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService loginService;

        /// <summary>
        /// Constructor to inject the dependency for concrete implementation of Login functionality
        /// </summary>
        /// <param name="loginService">Service having concrete implementation for Login functionality</param>
        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        /// <summary>
        /// HTTP Post method for validating the user against passed credentials
        /// </summary>
        /// <param name="request">Users credentials for validation</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            //Validating the users credentials
            var loginResponse = loginService.Login(request);
            if (loginResponse == null)
                //If credentials are not valid then returning as bad request 
                return BadRequest(new { message = "Please provide valid credentials" });

            //Returning the user object with valid JWTToken on successful credential validation
            return Ok(loginResponse);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Success");
        }
    }
}
