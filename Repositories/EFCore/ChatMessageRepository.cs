using Entities.Models;
using Repositories.Contracts;

namespace Repositories.EFCore;

public class ChatMessageRepository : RepositoryBase<ChatMessage>, IChatMessageRepository
{
    public ChatMessageRepository(RepositoryContext context) : base(context)
    {
    }
} 