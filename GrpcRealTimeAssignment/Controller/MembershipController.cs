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
    }
}
