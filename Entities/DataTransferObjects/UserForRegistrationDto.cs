namespace Entities.DataTransferObjects;

public class UserForRegistrationDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<string> Roles { get; set; }
    // Diğer gerekli alanlar eklenebilir
} 