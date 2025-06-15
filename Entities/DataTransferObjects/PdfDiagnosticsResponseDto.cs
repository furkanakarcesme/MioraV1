namespace Entities.DataTransferObjects;

public abstract record PdfDiagnosticsResponseBase(int AnalysisId, string Explanation, IEnumerable<string> Suggestions);

public record LabsPdfResponseDto(int AnalysisId, IEnumerable<LabObservationDto> Observations, string Explanation, IEnumerable<string> Suggestions)
    : PdfDiagnosticsResponseBase(AnalysisId, Explanation, Suggestions);
    
public record DiabetesPdfResponseDto(int AnalysisId, bool Prediction, string Explanation, IEnumerable<string> Suggestions)
    : PdfDiagnosticsResponseBase(AnalysisId, Explanation, Suggestions); 