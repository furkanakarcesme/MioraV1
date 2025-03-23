using Entities.Models;

namespace Repositories.Utilities;

public static class SlotGenerator
{
    public static List<Availability> GenerateSlotsForDoctor(int doctorId, DateTime start, DateTime end)
    {
        var slots = new List<Availability>();

        TimeSpan morningStart = new TimeSpan(9, 0, 0);
        TimeSpan morningEnd = new TimeSpan(12, 0, 0);
        TimeSpan afternoonStart = new TimeSpan(13, 0, 0);
        TimeSpan afternoonEnd = new TimeSpan(17, 0, 0);
        TimeSpan slotDuration = TimeSpan.FromMinutes(15);

        for (DateTime day = start.Date; day <= end.Date; day = day.AddDays(1))
        {
            // Sabah slotları
            for (var time = morningStart; time < morningEnd; time += slotDuration)
            {
                slots.Add(new Availability
                {
                    DoctorId = doctorId,
                    AvailableDate = day,
                    StartTime = time,
                    EndTime = time + slotDuration,
                    IsBooked = false,
                    IsDeleted = false
                });
            }

            // Öğleden sonra slotları
            for (var time = afternoonStart; time < afternoonEnd; time += slotDuration)
            {
                slots.Add(new Availability
                {
                    DoctorId = doctorId,
                    AvailableDate = day,
                    StartTime = time,
                    EndTime = time + slotDuration,
                    IsBooked = false,
                    IsDeleted = false
                });
            }
        }

        return slots;
    }
}