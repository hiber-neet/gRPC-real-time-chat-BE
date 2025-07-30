using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Service;

namespace GrpcRealTimeAssignment.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly RoomService _roomService;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            return Ok(await _roomService.GetAllRoomsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] Room room)
        {
            return Ok(await _roomService.CreateRoomAsync(room));
        }

		[HttpGet("{id}")]
		public async Task<IActionResult> GetRoomById(int id)
		{
			var room = await _roomService.GetRoomByIdAsync(id);
			return room == null ? NotFound() : Ok(room);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateRoom(int id, [FromBody] Room updatedRoom)
		{
			var result = await _roomService.UpdateRoomAsync(id, updatedRoom);
			return result == null ? NotFound() : Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteRoom(int id)
		{
			var success = await _roomService.DeleteRoomAsync(id);
			return success ? NoContent() : NotFound();
		}
	}

}
