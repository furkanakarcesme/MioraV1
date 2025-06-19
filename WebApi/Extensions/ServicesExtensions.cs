using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Extensions.Http;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;
using System.Net.Http.Headers;
using WebApi.Services;

namespace WebApi.Extensions;

public static class ServicesExtensions
{
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) => 
        services.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

    
    public static void ConfigureRepositoryManager(this IServiceCollection services) => 
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<IAuthService, AuthService>();
        
        services.AddScoped<IQuickPromptService, QuickPromptManager>();
        services.AddScoped<IChatService, ChatManager>();
        services.AddScoped<IPdfDiagnosticsService, PdfDiagnosticsManager>();
        services.AddScoped<IXRayDiagnosisService, XRayDiagnosisManager>();
        services.AddScoped<IImageStorageService, ImageStorageManager>();
        services.AddScoped<IPdfStorageService, PdfStorageManager>();

        // GPT Client: Geliştirme ortamında Mock, prodüksiyonda gerçek istemci kullanılabilir.
        // Şimdilik Mock'u doğrudan bağlıyoruz.
        services.AddHttpClient<IGptClientService, GptClientService>( (serviceProvider, client) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var apiKey = configuration["GptSettings:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("GPT API Key is not configured in user secrets.");
            }
            client.BaseAddress = new Uri("https://api.openai.com/v1/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        });
    }
    
    public static void ConfigurePythonClients(this IServiceCollection services, IConfiguration configuration)
    {
        // PDF Client
        var pdfApiSettings = configuration.GetSection("PdfApiSettings");
        var pdfBaseUrl = pdfApiSettings["BaseUrl"];
        if(string.IsNullOrEmpty(pdfBaseUrl))
            throw new InvalidOperationException("PDF API BaseUrl is not configured in appsettings.json");

        services.AddHttpClient<IPythonPdfClient, PythonPdfClient>(client =>
        {
            client.BaseAddress = new Uri(pdfBaseUrl);
        });
        
        // XRay Client
        var xrayApiSettings = configuration.GetSection("XRayApiSettings");
        var xrayBaseUrl = xrayApiSettings["BaseUrl"];
        if(string.IsNullOrEmpty(xrayBaseUrl))
            throw new InvalidOperationException("XRay API BaseUrl is not configured in appsettings.json");
        
        services.AddHttpClient<IPythonXrayClient, PythonXrayClient>(client =>
        {
            client.BaseAddress = new Uri(xrayBaseUrl);
        });
    }

    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDropdownRepository, DropdownRepository>();
        
        services.AddScoped<ILabObservationRepository, LabObservationRepository>();
        services.AddScoped<IChatSessionRepository, ChatSessionRepository>();
        services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
        services.AddScoped<IUploadRepository, UploadRepository>();
        services.AddScoped<IAnalysisResultRepository, AnalysisResultRepository>();
        services.AddScoped<IAiPromptLogRepository, AiPromptLogRepository>();
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        // ... existing code ...
    }
}