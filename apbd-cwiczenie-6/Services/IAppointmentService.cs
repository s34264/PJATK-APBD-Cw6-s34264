using apbd_cwiczenie_6.DTO;

namespace apbd_cwiczenie_6.Services;

public interface IAppointmentService
{ 
    public Task<List<AppointmentListDto>> getAllAppointments(string? status = null, string? patientLastName = null);
    public Task<bool> addAppointmentAsync(CreateAppointmentRequestDto appointment);
    public Task<bool> updateAppointmentAsync(UpdateAppointmentRequestDto appointment, int id);
    public Task<bool> deleteAppointmentAsync(int id);
}