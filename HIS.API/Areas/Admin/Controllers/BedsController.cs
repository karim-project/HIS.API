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
    public class BedsController : ControllerBase
    {
        private readonly IRepository<Bed> _bedRepository;

        public BedsController(IRepository<Bed> bedRepository)
        {
            _bedRepository = bedRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var beds =await _bedRepository.GetAsync(includes: [b => b.Room], tracked: false, cancellationToken: cancellationToken);

            var response = beds.Select(b => new BedResponse
            {
                Id =b!.Id,
                Number = b.BedNumber,
                RoomId = b.RoomId,
                RoomNumber =b.Room.Number,
            });
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id,CancellationToken cancellationToken) 
        {
            var bed =await _bedRepository.GetOneAsync(b => b.Id == id, includes: [b=>b.Room], tracked: false, cancellationToken: cancellationToken);

            if(bed is null)
                return NotFound();

            var response = new BedResponse
            {
                Id = bed.Id,
                Number = bed.BedNumber,
                RoomId = bed.RoomId,
                RoomNumber = bed.Room.Number,
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBedRequest createBedRequest , CancellationToken cancellationToken)
        {
            var bed =createBedRequest.Adapt<Bed>();

           await _bedRepository.AddAsync(bed ,cancellationToken);
           await _bedRepository.CommitAsync(cancellationToken);
            return CreatedAtAction(nameof(GetOne), new { id = bed.Id }, new
            {
                success_notifaction = "Bed Added Successfully"
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id , UpdateBedRequest updateBedRequest , CancellationToken cancellationToken)
        {
            var bed =await _bedRepository.GetOneAsync(b => b.Id == id, cancellationToken: cancellationToken);

            if(bed is  null)
                return NotFound();

            bed.BedNumber =updateBedRequest.BedNumber;
            bed.RoomId = updateBedRequest.RoomId;
            bed.IsOccupied = updateBedRequest.IsOccupied;

            _bedRepository.Update(bed);
           await _bedRepository.CommitAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id , CancellationToken cancellationToken)
        {
            var bed =await _bedRepository.GetOneAsync(b=>b.Id==id , cancellationToken:cancellationToken);

            if(bed is null) return NotFound();

            _bedRepository.Delete(bed);
            await _bedRepository.CommitAsync(cancellationToken);

            return NoContent();
        }
    }
}
