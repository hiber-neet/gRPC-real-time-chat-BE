using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Service;

namespace GrpcRealTimeAssignment.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembershipController : ControllerBase
    {
        private readonly MembershipService _service;
        public MembershipController(MembershipService service)
        {
            _service = service;
        }

        [HttpPost("join")]
        public async Task<IActionResult> Join([FromBody] Membership m)
        {
            return Ok(await _service.JoinRoomAsync(m.UserId, m.RoomId));
        }

		[HttpGet("room/{roomId}")]
		public async Task<IActionResult> GetMembersInRoom(int roomId)
		{
			return Ok(await _service.GetMembersInRoomAsync(roomId));
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateMembership(int id, [FromBody] Membership update)
		{
			var result = await _service.UpdateMembershipAsync(id, update);
			return result == null ? NotFound() : Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteMembership(int id)
		{
			var result = await _service.DeleteMembershipAsync(id);
			return result ? NoContent() : NotFound();
		}

	}
}
