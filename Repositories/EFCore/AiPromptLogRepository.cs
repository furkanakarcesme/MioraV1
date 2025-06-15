using Entities.Models;
using Repositories.Contracts;

namespace Repositories.EFCore;

public class AiPromptLogRepository : RepositoryBase<AiPromptLog>, IAiPromptLogRepository
{
    public AiPromptLogRepository(RepositoryContext context) : base(context)
    {
    }
} 