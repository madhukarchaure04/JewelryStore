using JewelryStore.API.DBModels;

namespace JewelryStore.API.Entities
{
    /// <summary>
    /// Enitity which includes details of data needed to pass after successful login
    /// </summary>
    public class LoginResponse: User
    {
        public string JWTToken { get; set; }
        public string Type { get; set; }
        public LoginResponse(User user, string JWTToken)
        {
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Username = user.Username;
            this.UserType = user.UserType;
            this.Type = user.UserType.ToString();
            this.JWTToken = JWTToken;
        }
    }
}
