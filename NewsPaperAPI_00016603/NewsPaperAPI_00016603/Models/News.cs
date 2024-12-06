namespace NewsPaperAPI_00016603.Models
{
    public class News
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime createdAt { get; set; } = DateTime.Now;

        public int CategoryId { get; set; }

        public Category Category { get; set; }


    }
}
