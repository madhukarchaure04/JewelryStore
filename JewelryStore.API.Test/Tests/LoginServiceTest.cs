using JewelryStore.API.DBModels;
using JewelryStore.API.Entities;
using JewelryStore.API.Helpers;
using JewelryStore.API.Services;
using JewelryStore.API.Test.Helpers;
using Microsoft.Extensions.Options;
using Xunit;

namespace JewelryStore.API.Test.Tests
{
    public class LoginServiceTest
    {
        private readonly ILoginService loginService;
        private readonly DBContext context;
        public LoginServiceTest()
        {
            this.context = MockDB.GetDBContext();
            IOptions<Setting> setting = Options.Create<Setting>(new Setting() { Key = Settings.Key});
            this.loginService = new LoginService(setting, context);
        }

        [Fact(DisplayName = "Valid user should login")]
        public void Valid_user_should_login()
        {
            LoginRequest request = new LoginRequest() { Username = "Alice", Password = "Alice" };
            var result = loginService.Login(request);
            Assert.True(result != null && result.JWTToken != null);
        }

        [Fact(DisplayName = "Invalid user should not login")]
        public void Invalid_user_should_not_login()
        {
            LoginRequest request = new LoginRequest() { Username = "Alice", Password = "Bob" };
            var result = loginService.Login(request);
            Assert.True(result == null);
        }
    }
}
