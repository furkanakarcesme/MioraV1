namespace Entities.Models;

public class ChatSession
{
    public int Id { get; set; }
    public int AnalysisId { get; set; }
    public AnalysisResult Analysis { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
} 