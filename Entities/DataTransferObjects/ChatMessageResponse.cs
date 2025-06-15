namespace Entities.DataTransferObjects;

public record ChatMessageResponse(string GptAnswer, IEnumerable<string> Suggestions); 