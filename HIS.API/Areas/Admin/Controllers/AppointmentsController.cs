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
    public class AppointmentsController : ControllerBase
    {
        private readonly IRepository<Appointment> _appointmentRepository;

        public AppointmentsController(IRepository<Appointment> appointmentRepository)
        {
           _appointmentRepository = appointmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var apps =await _appointmentRepository.GetAsync(includes: [a => a.Doctor, a => a.Patient], tracked: false, cancellationToken: cancellationToken);

            var result = apps.Select(a => new AppointmentResponse
            {
                Id =a!.Id,
                Date =a.CreatedAt,
                DoctorId=a.DoctorId,
                DoctorName = a.Doctor.FullName,
                PatientName=a.Patient.FullName,
                PatientId = a.PatientId
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id , CancellationToken cancellationToken)
        {
            var app =await _appointmentRepository.GetOneAsync(a => a.Id == id, includes: [a=>a.Doctor , a=>a.Patient], tracked: false, cancellationToken: cancellationToken);

            if (app is null)
                return NotFound();

            var result = new AppointmentResponse
            {
                Id = app.Id,
                Date = app.CreatedAt,
                DoctorName =app.Doctor.FullName,
                DoctorId =app.DoctorId,
                PatientId = app.PatientId,
                PatientName = app.Patient.FullName,
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentRequest createAppointmentRequest ,CancellationToken cancellationToken)
        {
            var app = createAppointmentRequest.Adapt<Appointment>();

           await _appointmentRepository.AddAsync(app);
           await _appointmentRepository.CommitAsync(cancellationToken);
            return CreatedAtAction(nameof(GetOne), new { id = app.Id }, new
            {
                success_notifaction = "Appointment Added Successfully"
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id , UpdateAppointmentRequest updateAppointmentRequest ,CancellationToken cancellationToken )
        {
            var app =await _appointmentRepository.GetOneAsync(a => a.Id == id, cancellationToken: cancellationToken);

            if(app is null)
                return NotFound();

            app.DoctorId = updateAppointmentRequest.DoctorId;
            app.PatientId = updateAppointmentRequest.PatientId;
            app.CreatedAt = updateAppointmentRequest.Date;


            _appointmentRepository.Update(app);
           await _appointmentRepository.CommitAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id , CancellationToken cancellationToken)
        {
            var app =await _appointmentRepository.GetOneAsync(a => a.Id == id, cancellationToken: cancellationToken);

            if(app is null)
                return NotFound();

            _appointmentRepository.Delete(app);
          await _appointmentRepository.CommitAsync(cancellationToken);

            return NoContent();
        }
    }
}
