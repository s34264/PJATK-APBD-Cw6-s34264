using apbd_cwiczenie_6.DTO;
using apbd_cwiczenie_6.Repositories;

namespace apbd_cwiczenie_6.Services;

public class AppointmentService : IAppointmentService
{
    public readonly IAppointmentRepository _appointmentRepository;
    
    public AppointmentService(IAppointmentRepository appointmentRepository)
    {
        this._appointmentRepository = appointmentRepository;
    }

    public async Task<List<AppointmentListDto>> getAllAppointments()
    {
        return await _appointmentRepository.getAllAppointmentsAsync();
    }

    public Task<bool> addAppointmentAsync(CreateAppointmentRequestDto appointment)
    {
        return _appointmentRepository.insertAppointmentAsync(appointment);
    }

    public Task<bool> updateAppointmentAsync(UpdateAppointmentRequestDto appointment)
    {
        throw new NotImplementedException();
    }

    public Task<bool> deleteAppointmentAsync(int id)
    {
        return _appointmentRepository.deleteAppointmentAsync(id);
    }
}