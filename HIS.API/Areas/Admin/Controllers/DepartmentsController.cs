using HIS.API.Models;
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
    public class DepartmentsController : ControllerBase
    {
        private readonly IRepository<Department> _departmentRepository;

        public DepartmentsController(IRepository<Department> departmentRepository) 
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var department =await _departmentRepository.GetAsync(tracked: false, cancellationToken: cancellationToken);

            var response = department.Select(d=>new DepartmentResponse
            {
                Id = d!.Id ,
                Name = d.Name,
                Description = d.Description
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id , CancellationToken cancellationToken)
        {
            var department =await _departmentRepository.GetOneAsync(d => d.Id == id, tracked: false, cancellationToken: cancellationToken);

            if (department is null)
                return NotFound();

            var response =  new DepartmentResponse
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description
            };

            return Ok(response);

        }

        [HttpPost]
        [Authorize(Roles = $"{SD.Super_Admin_Role} ,{SD.Admin_Role}")]
        public async Task<IActionResult> Create(CreateDepartmentRequest  createDepartmentRequest , CancellationToken cancellationToken)
        {
            Department department = createDepartmentRequest.Adapt<Department>();

           await _departmentRepository.AddAsync(department,cancellationToken);
           await _departmentRepository.CommitAsync(cancellationToken);
            return CreatedAtAction(nameof(GetOne), new { id = department.Id }, new 
            {
                success_notifaction = "Department Added Successfully"
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{SD.Super_Admin_Role} ,{SD.Admin_Role}")]
        public async Task<IActionResult> Edit(Guid id , UpdateDepartmentRequest  updateDepartmentRequest, CancellationToken cancellationToken)
        {
            var department =await _departmentRepository.GetOneAsync(d => d.Id == id, cancellationToken: cancellationToken);

            if (department is null)
                return NotFound();

            department.Name = updateDepartmentRequest.Name;
            department.Description = updateDepartmentRequest.Description;

            _departmentRepository.Update(department);
           await _departmentRepository.CommitAsync(cancellationToken);
                

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{SD.Super_Admin_Role} ,{SD.Admin_Role}")]
        public async Task<IActionResult> Delete(Guid id , CancellationToken cancellationToken)
        {
            var department =await _departmentRepository.GetOneAsync(d => d.Id == id, cancellationToken: cancellationToken);

            if (department is null)
                return NotFound();

            _departmentRepository.Delete(department);
           await _departmentRepository.CommitAsync(cancellationToken);

            return NoContent();
        }
    }
}
