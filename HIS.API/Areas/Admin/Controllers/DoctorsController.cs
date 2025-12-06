using HIS.API.DTOs.Request;
using HIS.API.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;

namespace HIS.API.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class DoctorsController : ControllerBase
    {
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly ILogger<DoctorsController> _logger;
        ApplicationDBcontext _context;
        public DoctorsController(ApplicationDBcontext context,IRepository<Doctor> doctorRepository,ILogger<DoctorsController> logger)
        {
            _doctorRepository = doctorRepository;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var doctors = await _doctorRepository.GetAsync(
                includes: [
                    d => d.Department!,d=>d.Specialty!
                ],
                tracked: false,
                cancellationToken: cancellationToken
            );

            var response = doctors.Select(d => new DoctorResponse
            {
                Id = d!.Id,
                FullName = d!.FullName,
                Email = d!.Email,
                Phone = d!.Phone,
                Img = d.Img,

                DepartmentId = d.DepartmentId,
                DepartmentName = d.Department!.Name,

                Specialties = d.DoctorSpecialties.Select(s => new DoctorSpecialtyResponse
                {
                    Id = s.SpecialtyId,
                    Name = s.Specialty!.Name
                }).ToList()
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id , CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetOneAsync(d => d.Id == id, includes: [d => d.Department!, d => d.DoctorSpecialties], cancellationToken: cancellationToken);

            if(doctor is null)
                return NotFound();

            var response = new DoctorResponse
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                Email = doctor.Email,
                Phone = doctor.Phone,
                Img = doctor.Img,

                DepartmentId = doctor.DepartmentId,
                DepartmentName = doctor.Department?.Name,

                Specialties = doctor.DoctorSpecialties.Select(s => new DoctorSpecialtyResponse
                {
                    Id = s.Specialty.Id,
                    Name = s.Specialty.Name
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPost]
        //[Authorize(Roles = $"{SD.Super_Admin_Role} ,{SD.Admin_Role}")]
        public async Task<IActionResult> Create([FromForm]CreateDoctorRequest createDoctorRequest , CancellationToken cancellationToken)
        {
            var transation = _context.Database.BeginTransaction();

         
            try
            {
                Doctor doctor = createDoctorRequest.Adapt<Doctor>();
                if (createDoctorRequest.Img is not null && createDoctorRequest.Img.Length > 0)
                {
                    var fillName = Guid.NewGuid().ToString() + Path.GetExtension(createDoctorRequest.Img.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\DoctorImg", fillName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await createDoctorRequest.Img.CopyToAsync(stream);
                    }
                    //save img
                    doctor.Img = fillName;
                }
                //save in DB
                var doctorCreated = await _doctorRepository.AddAsync(doctor, cancellationToken: cancellationToken);
               await _doctorRepository.CommitAsync(cancellationToken);

                transation.Commit();
                return CreatedAtAction(nameof(GetOne), new { id = doctor.Id }, new
                {
                    success_notifaction = "Add Doctor Successfully"
                });
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                transation.Rollback();

                return BadRequest(new ErrorModelResponse
                {
                    Code = "Error While Saving the product",
                    Description = ex.Message,
                });
            }

        }

        [HttpPut]
        [Authorize(Roles = $"{SD.Super_Admin_Role} ,{SD.Admin_Role}")]
        public async Task<IActionResult> Edit(Guid id , [FromBody]UpdateDoctorRequest updateDoctorRequest,CancellationToken cancellationToken)
        {
            var docterInDB =await _doctorRepository.GetOneAsync(d => d.Id == id, cancellationToken: cancellationToken);

            if (docterInDB is null)
                return NotFound(new 
                {
                msg = "Doctor not found"
                });

            if(updateDoctorRequest.Img is not null && updateDoctorRequest.Img.Length > 0)
            {
                var fillName = Guid.NewGuid().ToString() + Path.GetExtension(updateDoctorRequest.Img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\DoctorImg", fillName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await updateDoctorRequest.Img.CopyToAsync(stream);
                }
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\DoctorImg", docterInDB.Img!);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
                docterInDB.Img = fillName;
            }

            docterInDB.FullName = updateDoctorRequest.FullName!;
            docterInDB.Email = updateDoctorRequest.Email!;
            docterInDB.Phone = updateDoctorRequest.Phone;
            docterInDB.DepartmentId = updateDoctorRequest.DepartmentId;

           _doctorRepository.Update(docterInDB);
            await _doctorRepository.CommitAsync(cancellationToken);


            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{SD.Super_Admin_Role} ,{SD.Admin_Role}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {

            var doctor = await _doctorRepository.GetOneAsync(e => e.Id == id, cancellationToken: cancellationToken);
            if (doctor == null)
                return NotFound();
            // remove old photo
            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\DoctorImg", doctor.Img!);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            _doctorRepository.Delete(doctor);
            await _doctorRepository.CommitAsync(cancellationToken);

            return NoContent();
        }
    }
}
