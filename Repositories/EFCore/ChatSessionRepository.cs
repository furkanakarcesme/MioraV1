using Entities.Models;
using Repositories.Contracts;

namespace Repositories.EFCore;

public class ChatSessionRepository : RepositoryBase<ChatSession>, IChatSessionRepository
{
    public ChatSessionRepository(RepositoryContext context) : base(context)
    {
    }
} 