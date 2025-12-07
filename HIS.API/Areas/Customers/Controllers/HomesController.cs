using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HIS.API.Areas.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Area("Customers")]
   [Authorize]
    public class HomesController : ControllerBase
    {
        private readonly IRepository<Doctor> _doctorRepository;

        public HomesController(IRepository<Doctor> doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var doctors =await _doctorRepository.GetAsync(includes: [d=>d.Specialty! , e=>e.Department!] , tracked:false,cancellationToken:cancellationToken);

            return Ok(new
            {
                Doctor = doctors.AsEnumerable()
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Item(Guid id , CancellationToken cancellationToken)
        {
            var doctor =await _doctorRepository.GetOneAsync(d => d.Id == id, includes: [d => d.Specialty!]);

            if (doctor is null)
                return NotFound();

            var relatedDoctor = (await _doctorRepository.GetAsync(d => d.FullName.Contains(doctor.FullName) && d.Id != doctor.Id, includes: [d => d.SpecialtyId!])).Skip(0).Take(4);

            return Ok(new
            {
                Doctor = doctor,
                RelatedDoctor = relatedDoctor,
            });
        }
    }
}
