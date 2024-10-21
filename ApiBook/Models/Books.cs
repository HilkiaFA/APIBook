namespace LatihanJWT.Models
{
    public class Books
    {
        public string id { get; set; }
        public string book_title { get; set; }
        public string author { get; set; }
        public string publisher { get; set; }
        public int stock { get; set; }
        public int price { get; set; }
        public string id_user { get; set; }
    }
}
