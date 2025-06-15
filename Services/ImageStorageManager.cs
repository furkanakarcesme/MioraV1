using Microsoft.AspNetCore.Http;
using Services.Contracts;

namespace Services;

public class ImageStorageManager : IImageStorageService
{
    private readonly string _storagePath;

    public ImageStorageManager()
    {
        // wwwroot path'ini dinamik olarak alıp uploads/images klasörünü belirliyoruz.
        var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        _storagePath = Path.Combine(wwwRootPath, "uploads", "images");

        if (!Directory.Exists(_storagePath))
        {
            Directory.CreateDirectory(_storagePath);
        }
    }

    public async Task<string> SaveImageAsync(IFormFile imageFile)
    {
        if (imageFile is null || imageFile.Length == 0)
        {
            throw new ArgumentException("Image file is not provided or empty.");
        }

        var fileExtension = Path.GetExtension(imageFile.FileName);
        var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
        var filePath = Path.Combine(_storagePath, uniqueFileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await imageFile.CopyToAsync(stream);

        return filePath;
    }
} 