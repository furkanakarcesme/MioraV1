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
                You are a helpful medical assistant. Your task is to answer the user's question based on the provided analysis summary and the chat history. Provide your answer in Turkish, in a clear and concise way.

                ANALYSIS SUMMARY:
                {analysisExplanation}

                CHAT HISTORY (last 10 messages):
                {history}

                USER'S QUESTION:
                {userText}
                """;
    }
} 