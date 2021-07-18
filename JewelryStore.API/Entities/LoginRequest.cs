
namespace JewelryStore.API.Entities
{
    /// <summary>
    /// Entity required for making login POST API call
    /// </summary>
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
