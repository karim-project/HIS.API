using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HIS.API.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class PatientsController : ControllerBase
    {
        private readonly IRepository<Patient> _patientRepository;

        public PatientsController(IRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }
        [HttpGet("Get")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var Pations = await _patientRepository.GetAsync(includes: [p => p.Invoices!, p => p.Insurances!], tracked: false, cancellationToken: cancellationToken);

            return Ok(Pations);
        }
        [HttpGet("id")]
        public IActionResult GetOne(Guid id, CancellationToken cancellationToken)
        {
            var Patient = _patientRepository.GetOneAsync(p => p.Id == id, includes: [p => p.Appointments, p => p.MedicalRecordNumber], tracked: false, cancellationToken: cancellationToken);

            if (Patient is null)
                return NotFound();

            return Ok(Patient);
        }
        [HttpPost("")]
        [Authorize(Roles = $"{SD.Super_Admin_Role} ,{SD.Admin_Role}")]
        public async Task<IActionResult> Create([FromBody] CreatePationRequest createPationRequest, CancellationToken cancellationToken)
        {
            Patient patient = createPationRequest.Adapt<Patient>();

            await _patientRepository.AddAsync(patient, cancellationToken: cancellationToken);
            await _patientRepository.CommitAsync(cancellationToken);

            return CreatedAtAction(nameof(GetOne), new { id = patient.Id }, new
            {
                success_notifaction = "Add Patient Successfully"
            });
        }
        [HttpPut("{id}")]
        [Authorize(Roles = $"{SD.Super_Admin_Role} ,{SD.Admin_Role}")]
        public async Task<IActionResult> Edit(Guid id, UpdatePationRequest updatePationRequest, CancellationToken cancellationToken)
        {
            var pationInDB = await _patientRepository.GetOneAsync(p => p.Id == id, cancellationToken: cancellationToken);

            if (pationInDB is null)
                return NotFound();

            pationInDB.FullName = updatePationRequest.FullName;
            pationInDB.MedicalRecordNumber = updatePationRequest.MedicalRecordNumber;
            pationInDB.Phone = updatePationRequest.Phone;
            pationInDB.Address = updatePationRequest.Address;
            pationInDB.Email = updatePationRequest.Email;

            _patientRepository.Update(pationInDB);
            await _patientRepository.CommitAsync(cancellationToken);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id , CancellationToken cancellationToken)
        {
            var pationInDb =await _patientRepository.GetOneAsync(p => p.Id == id, cancellationToken: cancellationToken);

            if (pationInDb is null)
                return NotFound();

            _patientRepository.Delete(pationInDb);
           await _patientRepository.CommitAsync(cancellationToken);

            return NoContent();
        }
    }
}
