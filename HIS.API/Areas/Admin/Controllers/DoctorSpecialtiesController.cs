using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HIS.API.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class DoctorSpecialtiesController : ControllerBase
    {
        private readonly IRepository<DoctorSpecialty> _doctorSpecialtyRepository;

        public DoctorSpecialtiesController(IRepository<DoctorSpecialty> DoctorSpecialtyRepository)
        {
            _doctorSpecialtyRepository = DoctorSpecialtyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var doctorSpecialty =await _doctorSpecialtyRepository.GetAsync(includes: [d=>d.Doctor , d=>d.Specialty],tracked:false ,cancellationToken: cancellationToken);

            var response = doctorSpecialty.Select(d=>new DoctorSpecialtiesResponse
            {
                DoctorId =d!.DoctorId,
                DoctorName =d.Doctor.FullName,
                SpecialtyId =d.SpecialtyId,
                SpecialtyName =d.Specialty.Name
            });

            return Ok(response); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDoctorSpecialtyRequest createDoctorSpecialtyRequest , CancellationToken cancellationToken)
        {
            var reiation = createDoctorSpecialtyRequest.Adapt<DoctorSpecialty>();

           await _doctorSpecialtyRepository.AddAsync(reiation,cancellationToken);
           await _doctorSpecialtyRepository.CommitAsync(cancellationToken);

            return Ok(new { msg = "Specialty Assigned to Doctor Successfully" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid specialtyId , Guid doctorId , CancellationToken cancellationToken)
        {
            var relation =await _doctorSpecialtyRepository.GetOneAsync(r => r.DoctorId == doctorId && r.SpecialtyId == specialtyId, cancellationToken: cancellationToken);

            if (relation is null)
                return NotFound();

            _doctorSpecialtyRepository.Delete(relation);
           await _doctorSpecialtyRepository.CommitAsync(cancellationToken);


            return NoContent();
        }
    }
}
