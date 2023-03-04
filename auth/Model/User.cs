using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace auth.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }

    public class UserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }

    public class Jwt
    {
        public string key { get; set; }
        public string Issuser { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
    }
}
