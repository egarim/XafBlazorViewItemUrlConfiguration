using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XafBlazorViewItemUrlConfiguration.Module.Services;

namespace XafBlazorViewItemUrlConfiguration.Blazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly CopilotChatService _chatService;

        public ChatController(CopilotChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                status = "ready",
                timestamp = DateTime.UtcNow,
                service = "XAF Blazor Chat API"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Chat([FromBody] ChatRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Message))
            {
                return BadRequest(new { error = "Message is required" });
            }

            try
            {
                var response = await _chatService.AskAsync(request.Message);

                return Ok(new ChatResponse
                {
                    Message = response,
                    Timestamp = DateTime.UtcNow,
                    Context = request.Context
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Failed to process chat message",
                    details = ex.Message
                });
            }
        }
    }

    public class ChatRequest
    {
        public string Message { get; set; } = string.Empty;
        public Dictionary<string, string>? Context { get; set; }
    }

    public class ChatResponse
    {
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public Dictionary<string, string>? Context { get; set; }
    }
}
