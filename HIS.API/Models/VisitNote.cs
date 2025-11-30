namespace HIS.API.Models
{
    public class VisitNote
    {
        public Guid Id { get; set; } 
        public Guid VisitId { get; set; } 
        public Visit Visit { get; set; } = default!;
        public Guid AuthorId { get; set; }
        public string Content { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

    }
}
