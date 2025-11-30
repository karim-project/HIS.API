namespace HIS.API.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = default!;
        public string? Ward { get; set; }
        public int Capacity { get; set; }
        public ICollection<Bed> Beds { get; set; } = new List<Bed>();
    }
}
