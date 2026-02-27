using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using System.ComponentModel.DataAnnotations;

namespace XafCopilotStandalone.WebApi.Controllers
{
    /// <summary>
    /// REST API endpoint for AI chat interactions.
    /// Accepts messages and returns AI responses programmatically.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatClient _chatClient;
        private readonly ILogger<ChatController> _logger;

        public ChatController(IChatClient chatClient, ILogger<ChatController> logger)
        {
            _chatClient = chatClient;
            _logger = logger;
        }

        /// <summary>
        /// Send a message to the AI and get a response.
        /// </summary>
        /// <param name="request">Chat request with message and optional context</param>
        /// <returns>AI response</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ChatResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SendMessage([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest(new { error = "Message is required" });
            }

            try
            {
                _logger.LogInformation("Processing chat request: {Message}", request.Message);

                // Build conversation history
                var messages = new List<ChatMessage>
                {
                    new(ChatRole.System, BuildSystemPrompt(request.Context))
                };

                // Add conversation history if provided
                if (request.History != null && request.History.Any())
                {
                    foreach (var msg in request.History)
                    {
                        messages.Add(new ChatMessage(
                            msg.Role == "user" ? ChatRole.User : ChatRole.Assistant,
                            msg.Content
                        ));
                    }
                }

                // Add current message
                messages.Add(new(ChatRole.User, request.Message));

                // Get AI response
                var response = await _chatClient.CompleteAsync(messages);

                var result = new ChatResponse
                {
                    Message = response.Message.Text ?? string.Empty,
                    Timestamp = DateTime.UtcNow,
                    Model = response.ModelId ?? "unknown",
                    TokensUsed = response.Usage?.TotalTokenCount ?? 0
                };

                _logger.LogInformation("Chat response generated: {Tokens} tokens", result.TokensUsed);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing chat request");
                return StatusCode(500, new { error = "Failed to process chat request", details = ex.Message });
            }
        }

        /// <summary>
        /// Health check endpoint
        /// </summary>
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                status = "healthy",
                timestamp = DateTime.UtcNow,
                service = "chat-api"
            });
        }

        private string BuildSystemPrompt(Dictionary<string, string>? context)
        {
            var prompt = "You are an AI assistant integrated into a business application. " +
                        "Help users query data, create records, and answer questions.";

            if (context != null && context.Any())
            {
                prompt += "\n\nContext:\n";
                foreach (var kvp in context)
                {
                    prompt += $"- {kvp.Key}: {kvp.Value}\n";
                }
            }

            return prompt;
        }
    }

    /// <summary>
    /// Chat request model
    /// </summary>
    public class ChatRequest
    {
        /// <summary>
        /// User message
        /// </summary>
        [Required]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Optional context data (userId, orderId, etc.)
        /// </summary>
        public Dictionary<string, string>? Context { get; set; }

        /// <summary>
        /// Optional conversation history
        /// </summary>
        public List<HistoryMessage>? History { get; set; }
    }

    /// <summary>
    /// History message for conversation context
    /// </summary>
    public class HistoryMessage
    {
        public string Role { get; set; } = "user"; // "user" or "assistant"
        public string Content { get; set; } = string.Empty;
    }

    /// <summary>
    /// Chat response model
    /// </summary>
    public class ChatResponse
    {
        /// <summary>
        /// AI response message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Response timestamp
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Model used for generation
        /// </summary>
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Tokens consumed
        /// </summary>
        public int TokensUsed { get; set; }
    }
}
