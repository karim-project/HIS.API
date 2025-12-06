using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HIS.API.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class SpecialtiesController : ControllerBase
    {
        private readonly IRepository<Specialty> _specialtyRepository;

        public SpecialtiesController(IRepository<Specialty> specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var specialties = await _specialtyRepository.GetAsync(tracked: false, cancellationToken: cancellationToken);

            var response = specialties.Select(s => new SpecialtyResponse
            {
                Id = s!.Id,
                Name = s.Name,
                Description = s.Description

            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id, CancellationToken cancellationToken)
        {
            var specialty = await _specialtyRepository.GetOneAsync(s => s.Id == id, tracked: false, cancellationToken: cancellationToken);

            if (specialty is null)
                return NotFound();

            var response = new SpecialtyResponse
            {
                Id = specialty.Id,
                Name = specialty.Name,
                Description = specialty.Description

            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateSpecialtyRequest createSpecialtyRequest, CancellationToken cancellationToken)
        {
            Specialty specialty = createSpecialtyRequest.Adapt<Specialty>();

            await _specialtyRepository.AddAsync(specialty, cancellationToken: cancellationToken);
            await _specialtyRepository.CommitAsync(cancellationToken);

            return CreatedAtAction(nameof(GetOne), new { id = specialty.Id }, new
            {
                success_notifaction = "Specialty Added Successfully"
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, UpdateSpecialtyRequest updateSpecialtyRequest, CancellationToken cancellationToken)
        {
            var specialty = await _specialtyRepository.GetOneAsync(s => s.Id == id, cancellationToken: cancellationToken);

            if (specialty is null)
                return NotFound();

            specialty.Name = updateSpecialtyRequest.Name;
            specialty.Description = updateSpecialtyRequest.Description;

            _specialtyRepository.Update(specialty);
            await _specialtyRepository.CommitAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id , CancellationToken cancellationToken)
        {
            var specialty =await _specialtyRepository.GetOneAsync(s => s.Id == id, cancellationToken: cancellationToken);

            if (specialty is null)
                return NotFound();

            _specialtyRepository.Delete(specialty);
           await _specialtyRepository.CommitAsync(cancellationToken);

            return NoContent();
        }
    }
}
