using Microsoft.AspNetCore.Http;

namespace Services.Contracts;

public interface IImageStorageService
{
    Task<string> SaveImageAsync(IFormFile imageFile);
} 