namespace Voom.DroneNews.API.Models
{
    public class Article
    {
        public Guid ID { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Source { get; set; }

        public Article()
        {
            ID = Guid.NewGuid();
        }
    }
}
