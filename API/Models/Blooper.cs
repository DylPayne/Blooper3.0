namespace API.Models
{
    public class Blooper
    {
        public int id { get; set; }
        public string word { get; set; }

        public Blooper(int id, string word)
        {
            this.id = id;
            this.word = word;
        }

        public Blooper()
        {
        }
    }
}
