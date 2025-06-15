using Entities.Models;
using Repositories.Contracts;

namespace Repositories.EFCore;

public class AnalysisResultRepository : RepositoryBase<AnalysisResult>, IAnalysisResultRepository
{
    public AnalysisResultRepository(RepositoryContext context) : base(context)
    {
    }
} 