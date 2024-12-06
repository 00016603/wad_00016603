using NewsPaperAPI_00016603.Models;

namespace NewsPaperAPI_00016603.DTOs
{
    public class NewsDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? createdAt { get; set; } = DateTime.Now;

        public int CategoryId { get; set; }

        public CategoryDTO? Category { get; set; }
    }
}
