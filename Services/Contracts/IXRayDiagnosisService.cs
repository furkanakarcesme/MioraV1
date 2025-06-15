using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;

namespace Services.Contracts;

public interface IXRayDiagnosisService
{
    Task<XRayResponseDto> PerformDiagnosisAsync(IFormFile imageFile, int userId);
} 