using Entities.DataTransferObjects;
using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Services.Contracts;
using Services.Utilities;
using System.Text;

namespace Services;

public class ChatManager : IChatService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IGptClientService _gptClient;

    public ChatManager(IRepositoryManager repositoryManager, IGptClientService gptClient)
    {
        _repositoryManager = repositoryManager;
        _gptClient = gptClient;
    }

    public async Task<ChatMessageResponse> ProcessUserMessageAsync(ChatMessageRequest request, int userId)
    {
        // 1. İlgili Analizi ve Oturumu Bul veya Oluştur
        var analysis = await _repositoryManager
            .AnalysisResult
            .FindByCondition(a => a.Id == request.AnalysisId, trackChanges: false)
            .SingleOrDefaultAsync();

        if (analysis is null)
        {
            throw new Exception($"Analysis with ID {request.AnalysisId} not found.");
        }

        var session = await _repositoryManager
            .ChatSession
            .FindByCondition(cs => cs.AnalysisId == request.AnalysisId, trackChanges: true)
            .Include(cs => cs.ChatMessages)
            .SingleOrDefaultAsync();

        if (session is null)
        {
            session = new ChatSession { AnalysisId = request.AnalysisId, UserId = userId };
            _repositoryManager.ChatSession.Create(session);
        }

        // 2. Kullanıcı Mesajını Kaydet
        var userMessage = new ChatMessage
        {
            SessionId = session.Id,
            IsUser = true,
            Text = request.Text,
            CreatedAt = DateTime.UtcNow
        };
        session.ChatMessages.Add(userMessage);
        await _repositoryManager.SaveAsync();

        // 3. Sohbet Geçmişini Oluştur (son 10 mesaj)
        var history = new StringBuilder();
        foreach (var msg in session.ChatMessages.OrderByDescending(m => m.CreatedAt).Take(10).Reverse())
        {
            history.AppendLine($"{(msg.IsUser ? "User" : "Assistant")}: {msg.Text}");
        }
        
        // 4. GPT Prompt'unu Oluştur
        var prompt = PromptFactory.BuildChatPrompt(analysis.ResultSummary!, history.ToString(), request.Text);

        // 5. GPT'yi Çağır
        var (gptAnswer, suggestions, tokens) = await _gptClient.GetResponseAsync(prompt);

        // 6. AI Yanıtını Kaydet
        var assistantMessage = new ChatMessage
        {
            SessionId = session.Id,
            IsUser = false,
            Text = gptAnswer,
            CreatedAt = DateTime.UtcNow,
            AiTokens = tokens
        };
        session.ChatMessages.Add(assistantMessage);
        await _repositoryManager.SaveAsync();
        
        // 7. Yanıtı ve yeni önerileri döndür
        return new ChatMessageResponse(gptAnswer, suggestions);
    }
} 