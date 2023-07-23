namespace API.Models
{
    public class Blooper
    {
        public int id { get; set; }
        public string name { get; set; }

        public Blooper(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public Blooper()
        {
        }
    }
}
