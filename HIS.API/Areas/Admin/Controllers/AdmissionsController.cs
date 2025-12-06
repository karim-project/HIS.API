using HIS.API.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HIS.API.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class AdmissionsController : ControllerBase
    {
        private readonly IRepository<Admission> _admissionRepository;

        public AdmissionsController(IRepository<Admission> admissionRepository)
        {
            _admissionRepository = admissionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var adm =await _admissionRepository.GetAsync(includes: [a => a.Patient, a => a.Room!, a => a.Bed!] ,tracked:false,cancellationToken:cancellationToken);

            var result = adm.Select(r => new AdmissionResponse
            {
                Id=r!.Id,
                AdmissionDate =r.AdmitAt,
                PatientId =r.PatientId,
                PatientName = r.Patient.FullName,
                RoomId = r.RoomId!,
                RoomNumber = r.Room!.Number,
                BedId =r.BedId,
                BedNumber = r.Bed!.BedNumber
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id , CancellationToken cancellationToken)
        {
            var adm =await _admissionRepository.GetOneAsync(a=>a.Id==id, includes: [a=>a.Patient , a => a.Room!, a => a.Bed!],tracked:false,cancellationToken: cancellationToken);

            if(adm is null)
                return NotFound();

            var result = new AdmissionResponse 
            {
                Id=adm.Id,
                AdmissionDate=adm.AdmitAt,
                PatientId=adm.PatientId,
                PatientName =adm.Patient.FullName,
                RoomId=adm.RoomId,
                RoomNumber =adm.Room!.Number,
                BedId =adm.BedId,
                BedNumber =adm.Bed!.BedNumber
            };


            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdmissionRequest createAdmissionRequest, CancellationToken cancellationToken)
        {
            var adm = createAdmissionRequest.Adapt<Admission>();

            await _admissionRepository.AddAsync(adm,cancellationToken);
            await _admissionRepository.CommitAsync(cancellationToken);
            return CreatedAtAction(nameof(GetOne), new { id = adm.Id }, new
            {
                success_notifaction = "Admission Added Successfully"
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, UpdateAdmissionRequest updateAdmissionRequest, CancellationToken cancellationToken)
        {
            var adm = await _admissionRepository.GetOneAsync(a => a.Id == id, cancellationToken: cancellationToken);

            if (adm is null)
                return NotFound();

            adm.PatientId = updateAdmissionRequest.PatientId;
            adm.RoomId = updateAdmissionRequest.RoomId;
            adm.BedId = updateAdmissionRequest.BedId;
            adm.AdmitAt = updateAdmissionRequest.AdmitAt;
            adm.DischargeAt = updateAdmissionRequest.DischargeAt;

            _admissionRepository.Update(adm);
            await _admissionRepository.CommitAsync(cancellationToken);

            return NoContent();
        }
    }
}
