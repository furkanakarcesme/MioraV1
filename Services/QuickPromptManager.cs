using Entities.DataTransferObjects;
using Entities.Enums;
using Services.Contracts;

namespace Services;

public class QuickPromptManager : IQuickPromptService
{
    private static readonly Dictionary<QuickPromptType, string[]> Suggestions = new()
    {
        [QuickPromptType.XRay] = new[]
        {
            "Randevu almak istiyorum",
            "Hangi egzersizleri yapmalıyım?"
        },
        [QuickPromptType.Diabetes] = new[]
        {
            "Diyabet için beslenme önerisi",
            "İlaç tedavisi hakkında bilgi"
        },
        [QuickPromptType.Labs] = new[]
        {
            "Eksik değerler nasıl yükseltilir?",
            "Hangi doktora gitmeliyim?"
        }
    };

    public QuickPromptResponse GetSuggestions(QuickPromptType type)
    {
        if (Suggestions.TryGetValue(type, out var suggestions))
        {
            return new QuickPromptResponse(suggestions);
        }

        return new QuickPromptResponse(Array.Empty<string>());
    }
} 