using System.ComponentModel.DataAnnotations.Schema;

namespace HIS.API.Models
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string EmployeeNumber { get; set; } = default!;
        public string FullName { get; set; } = default!;
       
        public string? Phone { get; set; } 
        public string? Email { get; set; }
        public string? Img { get; set; }

      
        public ICollection<DoctorSpecialty> DoctorSpecialties { get; set; } = new List<DoctorSpecialty>();

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();

        public Guid? DepartmentId { get; set; }

        public Department? Department { get; set; }

      

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
