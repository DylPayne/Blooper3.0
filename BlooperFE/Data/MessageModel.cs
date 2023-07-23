namespace BlooperFE.Data
{
    public class MessageModel
    {
        public string text { get; set; }
        public string to_id { get; set; }
        public int from_id { get; set; }

        public MessageModel()
        {

        }
        public MessageModel(string text, string to_id, string from_id)
        {
            this.text = text;
            this.to_id = to_id;
            this.from_id = int.Parse(from_id);
        }
    }
}
