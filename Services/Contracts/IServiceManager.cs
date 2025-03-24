namespace Services.Contracts;

public interface IServiceManager
{
    IAppointmentService AppointmentService { get; }
    IAvailabilityService AvailabilityService { get; }
}