namespace Services.Utilities;

public static class PromptFactory
{
    private const string InitialPromptSuffix = """

        ### RESPONSE FORMAT ###
        YOU MUST FOLLOW THIS FORMAT EXACTLY:
        1. Provide your analysis summary in Turkish.
        2. Then, on a new line, write the exact separator: |||
        3. After the separator, provide 2-3 common questions a user might ask about these results. Each question must be on a new line.
        """;
    
    public static string BuildXrayPrompt(string jsonPayload)
    {
        return $"""
                Aşağıda röntgen analizi sonucu var. {jsonPayload}
                Hastaya anlaşılır Türkçe ile açıkla;
                hasta ise doktora yönlendir, değilse koruyucu öneri ekle.
                """ + InitialPromptSuffix;
    }

    public static string BuildLabsPrompt(string jsonPayload)
    {
        return $"""
                Aşağıda hastanın laboratuvar değerleri ve
                düşük/normal/yüksek etiketleri yer alıyor.
                Hastanın anlayacağı Türkçe ile açıkla,
                riskli değerler için kısa öneriler ekle.
                {jsonPayload}
                """ + InitialPromptSuffix;
    }

    public static string BuildDiabetesPrompt(string jsonPayload)
    {
        return $"""
                Aşağıda ML modelinin diyabet tahmini var
                (1 = olası diyabet, 0 = negatif).
                Sonuca göre anlaşılır tavsiyeler ver.
                {jsonPayload}
                """ + InitialPromptSuffix;
    }
    
    public static string BuildChatPrompt(string analysisExplanation, string history, string userText)
    {
        return $"""
                ### ROLE & GOAL ###
                You are a helpful medical assistant. Your goal is to interpret lab results in a way that is scientific but easy for a layperson to understand. 
                You must NOT cause panic. You should provide general, safe advice and guide the user on next steps, such as which specialist to see. 
                You must NOT create detailed diet or exercise plans, but you can give simple examples of foods or activities that might help (e.g., "reducing red meat consumption can be beneficial for high LDL").

                ### PRIMARY TASK ###
                Your main task is to answer the LATEST `USER'S QUESTION` below. 
                Use the `ANALYSIS SUMMARY` and `CHAT HISTORY` ONLY as context to provide a relevant and accurate answer. 
                Do not repeat the analysis summary unless the user specifically asks for it.

                ### CONTEXT ###
                ANALYSIS SUMMARY:
                {analysisExplanation}

                CHAT HISTORY (last 10 messages):
                {history}

                USER'S QUESTION:
                {userText}
                
                ### RESPONSE FORMAT ###
                YOU MUST FOLLOW THIS FORMAT EXACTLY:
                1. Provide your answer to the user's question in Turkish.
                2. Then, on a new line, write the exact separator: |||
                3. After the separator, provide 2-3 follow-up questions a user might ask. Each question must be on a new line.

                Example:
                Cevabınız burada yer alacak.
                |||
                İlk öneri soru.
                İkinci öneri soru.
                """;
    }
} 