using JewelryStore.API.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JewelryStore.API.DBModels
{
    /// <summary>
    /// User entity details
    /// </summary>
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Key]
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public UserType UserType { get; set; }
    }
}
