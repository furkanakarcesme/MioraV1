namespace Entities.DataTransferObjects;

public record UserDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Email { get; init; }
    public ICollection<string> Roles { get; init; } = new List<string>();
} 