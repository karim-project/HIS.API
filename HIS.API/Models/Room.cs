namespace HIS.API.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = default!;
       
        public int FloorNumber { get; set; }
        public int Capacity { get; set; }

        public Guid DepartmentId { get; set; }
        public Department? Department { get; set; }
        public ICollection<Bed> Beds { get; set; } = new List<Bed>();
    }
}
