using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HIS.API.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class RoomsController : ControllerBase
    {
        private readonly IRepository<Room> _roomRepository;

        public RoomsController(IRepository<Room> roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var rooms = await _roomRepository.GetAsync(includes: [r => r.Department!], tracked: false, cancellationToken: cancellationToken);

            var response = rooms.Select(r => new RoomResponse
            {
                Id = r!.Id,
                DepartmentId = r.DepartmentId,
                DepartmentName = r.Department?.Name,
                Number = r.Number,
                Capacity = r.Capacity,
                FloorNumber = r.FloorNumber,
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetOneAsync(r => r.Id == id, includes: [r => r.Department!], cancellationToken: cancellationToken);

            if (room is null)
                return NotFound();

            var reponse = new RoomResponse
            {
                Id = room.Id,
                Number = room.Number,
                Capacity = room.Capacity,   
                FloorNumber = room.FloorNumber,
                DepartmentId = room.DepartmentId,
                DepartmentName = room.Department?.Name,

            };

            return Ok(reponse);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoomRequest createRoomRequest, CancellationToken cancellationToken)
        {
            var room = createRoomRequest.Adapt<Room>();

            await _roomRepository.AddAsync(room, cancellationToken);
            await _roomRepository.CommitAsync(cancellationToken);
            return CreatedAtAction(nameof(GetOne), new { Id = room.Id }, new
            {
                success_notifaction = "Room Added Successfully"
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id , UpdateRoomRequest updateRoomRequest , CancellationToken cancellationToken)
        {
            var room =await _roomRepository.GetOneAsync(r => r.Id == id, cancellationToken: cancellationToken);

            if(room is null)
                return NotFound();

            room.Number = updateRoomRequest.Number;
            room.DepartmentId = updateRoomRequest.DepartmentId;
            room.FloorNumber = updateRoomRequest.FloorNumber;
            room.Capacity = updateRoomRequest.Capacity;

            _roomRepository.Update(room);
           await _roomRepository.CommitAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id,CancellationToken cancellationToken)
        {
            var room =await _roomRepository.GetOneAsync(r => r.Id == id, cancellationToken: cancellationToken);

            if(room is null)
                return NotFound();

            _roomRepository.Delete(room);
           await _roomRepository.CommitAsync(cancellationToken);

            return NoContent();
        }
    }
}
