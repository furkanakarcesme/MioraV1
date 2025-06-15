namespace Entities.DataTransferObjects;

public record LabObservationDto(
    string Name,
    double Value
    // string Status // Kullanıcının isteği üzerine geçici olarak kaldırıldı.
); 