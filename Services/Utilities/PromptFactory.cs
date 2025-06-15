namespace Services.Utilities;

public static class PromptFactory
{
    public static string BuildXrayPrompt(string jsonPayload)
    {
        return $"""
                Aşağıda röntgen analizi sonucu var. {jsonPayload}
                Hastaya anlaşılır Türkçe ile açıkla;
                hasta ise doktora yönlendir, değilse koruyucu öneri ekle.
                """;
    }

    public static string BuildLabsPrompt(string jsonPayload)
    {
        return $"""
                Aşağıda hastanın laboratuvar değerleri ve
                düşük/normal/yüksek etiketleri yer alıyor.
                Hastanın anlayacağı Türkçe ile açıkla,
                riskli değerler için kısa öneriler ekle.
                {jsonPayload}
                """;
    }

    public static string BuildDiabetesPrompt(string jsonPayload)
    {
        return $"""
                Aşağıda ML modelinin diyabet tahmini var
                (1 = olası diyabet, 0 = negatif).
                Sonuca göre anlaşılır tavsiyeler ver.
                {jsonPayload}
                """;
    }
    
    public static string BuildChatPrompt(string analysisExplanation, string history, string userText)
    {
        return $"""
                CONTEXT:
                {analysisExplanation}

                CHAT HISTORY (son 10 mesaj):
                {history}

                USER:
                {userText}
                
                ASSISTANT_MUST: Türkçe, kısa, anlaşılır öneriler ver.
                """;
    }
} 