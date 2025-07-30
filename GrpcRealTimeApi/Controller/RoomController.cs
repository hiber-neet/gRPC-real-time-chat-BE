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
    }

}
