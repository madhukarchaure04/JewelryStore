using JewelryStore.API.Helpers;
using JewelryStore.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.API.Authorization
{
    public class JWTTokenMiddleware
    {
        private readonly Setting setting;
        private readonly RequestDelegate request;

        public JWTTokenMiddleware(IOptions<Setting> setting, RequestDelegate request)
        {
            this.setting = setting.Value;
            this.request = request;
        }

        public async Task Invoke(HttpContext context, ILoginService loginService)
        {
            //For each API request having Authorize attribute, it will check and parse the JWT token from request header
            var jwtToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (jwtToken != null)
                //If the JWTToken is passed with the request, validate the token
                SetUserContext(jwtToken, context, loginService);

            await request(context);
        }
        /// <summary>
        /// Sets the HTTPContext with user who has made the request or null in case validation fails
        /// </summary>
        /// <param name="jwtToken">The token passed in header for API call</param>
        /// <param name="context">HTTP context of current call</param>
        /// <param name="loginService">Service having implemenation for Login functionality</param>
        private void SetUserContext(string jwtToken, HttpContext context, ILoginService loginService)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(setting.Key);
                var handler = new JwtSecurityTokenHandler();
                //Validating the JWTTOken on below paramenters
                handler.ValidateToken(jwtToken, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedJWTToken);
                var validatedToken = (JwtSecurityToken)validatedJWTToken;
                //Fetching the username from the token to know the user who has requested the API
                var username = validatedToken.Claims.First(v => v.Type == "username").Value;

                //Storing the data in User key in HTTPContext for future use
                context.Items["User"] = loginService.GetUser(username);
            }
            catch
            {
                //If JWTToken is not valid, setting User in HTTPContext as null
                context.Items["User"] = null;
            }
        }
    }
}
