using Entities.Enums;
using Entities.DataTransferObjects;

namespace Services.Contracts;

public interface IQuickPromptService
{
    QuickPromptResponse GetSuggestions(QuickPromptType type);
} 