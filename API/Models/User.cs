namespace API.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }

        public User(int id, string username)
        {
            this.id = id;
            this.username = username;
        }
    }
}
