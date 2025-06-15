using Entities.Models;
using Repositories.Contracts;

namespace Repositories.EFCore;

public class UploadRepository : RepositoryBase<UploadBase>, IUploadRepository
{
    public UploadRepository(RepositoryContext context) : base(context)
    {
    }

    public void CreateUpload(UploadBase upload) => Create(upload);
} 