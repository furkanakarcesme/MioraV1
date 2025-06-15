using Entities.DataTransferObjects;

namespace Services.Contracts;

public interface IChatService
{
    Task<ChatMessageResponse> ProcessUserMessageAsync(ChatMessageRequest request, int userId);
} 