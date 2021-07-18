using JewelryStore.API.DBModels;
using JewelryStore.API.Entities;

namespace JewelryStore.API.Services
{
    /// <summary>
    /// Interface defining the template required for LoginService
    /// </summary>
    public interface ILoginService
    {
        LoginResponse Login(LoginRequest loginRequest);
        User GetUser(string username);
    }
}
