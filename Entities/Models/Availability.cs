namespace Entities.Models;

public class Availability
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public User Doctor { get; set; }

    public DateTime AvailableDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public bool IsDeleted { get; set; } = false;
    public bool IsBooked { get; set; } = false;
}


/*
public class Availability
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public User Doctor { get; set; } // Doktor ID'si (User tablosundan)

    public DateTime AvailableDate { get; set; } // Hangi gün?
    public TimeSpan StartTime { get; set; } // Başlangıç saati
    public TimeSpan EndTime { get; set; } // Bitiş saati

    public bool IsDeleted { get; set; } = false; // Admin tarafından tamamen iptal mi?
    public bool IsBooked { get; set; } = false; // Hasta tarafından alındı mı?
}
*/