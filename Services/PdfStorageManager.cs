using Microsoft.AspNetCore.Http;
using Services.Contracts;

namespace Services;

public class PdfStorageManager : IPdfStorageService
{
    private readonly string _storagePath;

    public PdfStorageManager()
    {
        var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        _storagePath = Path.Combine(wwwRootPath, "uploads", "pdfs");

        if (!Directory.Exists(_storagePath))
        {
            Directory.CreateDirectory(_storagePath);
        }
    }

    public async Task<(string FilePath, int Pages)> SavePdfAsync(IFormFile pdfFile)
    {
        if (pdfFile is null || pdfFile.Length == 0)
        {
            throw new ArgumentException("PDF file is not provided or empty.");
        }

        var uniqueFileName = $"{Guid.NewGuid()}.pdf";
        var filePath = Path.Combine(_storagePath, uniqueFileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await pdfFile.CopyToAsync(stream);

        // Not: Gerçek sayfa sayısını almak için PdfPig veya iTextSharp gibi
        // bir kütüphane kullanmak gerekir. Şimdilik 0 dönüyoruz.
        int pageCount = 0; 

        return (filePath, pageCount);
    }
} 