using Entities.Models;

namespace Repositories.Contracts;

public interface IUploadRepository : IRepositoryBase<UploadBase>
{
    // Upload işlemleri için özel metotlar buraya eklenebilir.
    void CreateUpload(UploadBase upload);
} 