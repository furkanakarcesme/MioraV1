namespace Entities.Models;

public class ChatMessage
{
    public int Id { get; set; }
    public int SessionId { get; set; }
    public ChatSession Session { get; set; }
    public bool IsUser { get; set; }
    public string Text { get; set; }
    public int AiTokens { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
} 