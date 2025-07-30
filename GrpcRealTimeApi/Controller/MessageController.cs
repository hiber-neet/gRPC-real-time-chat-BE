using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Service;

namespace GrpcRealTimeAssignment.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _service;

        public MessageController(MessageService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] Message msg)
        {
            return Ok(await _service.SendMessageAsync(msg));
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetMessages(int roomId)
        {
            return Ok(await _service.GetMessagesByRoom(roomId));
        }

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateMessage(int id, [FromBody] string newContent)
		{
			var result = await _service.UpdateMessageAsync(id, newContent);
			return result == null ? NotFound() : Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteMessage(int id)
		{
			var result = await _service.DeleteMessageAsync(id);
			return result ? NoContent() : NotFound();
		}

	}

}
