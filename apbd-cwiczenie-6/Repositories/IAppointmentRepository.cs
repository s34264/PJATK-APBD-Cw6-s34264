using apbd_cwiczenie_6.Models;
namespace apbd_cwiczenie_6.Repositories;
using apbd_cwiczenie_6.DTO;

public interface IAppointmentRepository
{
    public Task<List<AppointmentListDto>> getAllAppointmentsAsync();
    public Task<bool> insertAppointmentAsync(CreateAppointmentRequestDto appointment);
    public Task<bool> deleteAppointmentAsync(int id);
    public Task<bool> updateAppointmentAsync(UpdateAppointmentRequestDto appointment, int id);
}