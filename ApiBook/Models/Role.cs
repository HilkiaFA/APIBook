namespace LatihanJWT.Models
{
    public class Role
    {
        public string id { get; set; }
        public string title { get; set; }
        public ICollection<Users> users { get; set; }
    }
}
