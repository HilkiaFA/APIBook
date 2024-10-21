using System.Text.Json.Serialization;

namespace LatihanJWT.Models
{
    public class Users
    {
        public string id { get; set; }
        public string roles_id { get; set; }
        public string email { get; set; }
        public string password_user { get; set; }
        public string name_user { get; set; }
        public DateTime birthdate { get; set; }
        public string status_user { get; set; }
        [JsonIgnore]
        public Role roles { get; set; }
    }
}
