namespace Services.Contracts;

public interface IServiceManager
{
    IAppointmentService AppointmentManager { get; }
    IAvailabilityService AvailabilityManager { get; }
}