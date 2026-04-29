using apbd_cwiczenie_6.Models;
namespace apbd_cwiczenie_6.Repositories;
using apbd_cwiczenie_6.DTO;

public interface IAppointmentRepository
{
    public Task<List<AppointmentListDto>> getAllAppointmentsAsync(string? status = null, string? patientLastName = null);
    public Task<AppointmentDetailsDto> getAppointmentByIdAsync(int id);
    public Task<bool> insertAppointmentAsync(CreateAppointmentRequestDto appointment);
    public Task<bool> deleteAppointmentAsync(int id);
    public Task<bool> updateAppointmentAsync(UpdateAppointmentRequestDto appointment, int id);
}