using JewelryStore.API.DBModels;
using JewelryStore.API.Entities;
using JewelryStore.API.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JewelryStore.API.Services
{
    /// <summary>
    /// Concrete implemetation for LoginService
    /// </summary>
    public class LoginService : ILoginService
    {
        private readonly Setting setting;
        private readonly DBContext context;

        public LoginService(IOptions<Setting> setting, DBContext context)
        {
            this.setting = setting.Value;
            this.context = context;
        }

        /// <summary>
        /// Returns the User object against the Username
        /// </summary>
        /// <param name="username">Username for whom other details needs to fetched from DB</param>
        /// <returns></returns>
        public User GetUser(string username)
        {
            return context.Users.FirstOrDefault(c => string.Compare(c.Username, username, true) == 0);
        }

        /// <summary>
        /// Performs the credetials validation of user.
        /// </summary>
        /// <param name="loginRequest">User credentails received with API request</param>
        /// <returns></returns>
        public LoginResponse Login(LoginRequest loginRequest)
        {
            //Username is case sensitive but password has to be exact
            User user = context.Users.FirstOrDefault(c => string.Compare(c.Username, loginRequest.Username, true) == 0 && c.Password == loginRequest.Password);
            if (user == null)
                return null;
            string jwtTOken = GenerateJWTToken(user);
            
            //If user credentials are valid return the User object along with JWTToken
            return new LoginResponse(user, jwtTOken);
        }

        /// <summary>
        /// Generate the JWT token against user
        /// </summary>
        /// <param name="user">User details who has requested the JWTToken</param>
        /// <returns></returns>
        private string GenerateJWTToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(setting.Key);
            var handler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("username", user.Username) }),
                Expires = DateTime.UtcNow.AddHours(2), // Expires the token after two hours
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtToken = handler.CreateToken(descriptor);
            return handler.WriteToken(jwtToken);
        }
    }
}
