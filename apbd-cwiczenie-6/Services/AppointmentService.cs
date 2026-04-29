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

    public async Task<List<AppointmentListDto>> getAllAppointments(string? status = null, string? patientLastName = null)
    {
        return await _appointmentRepository.getAllAppointmentsAsync(status, patientLastName);
    }

    public Task<bool> addAppointmentAsync(CreateAppointmentRequestDto appointment)
    {
        return _appointmentRepository.insertAppointmentAsync(appointment);
    }

    public Task<bool> updateAppointmentAsync(UpdateAppointmentRequestDto appointment, int id)
    {
        return _appointmentRepository.updateAppointmentAsync(appointment, id);
    }

    public Task<bool> deleteAppointmentAsync(int id)
    {
        return _appointmentRepository.deleteAppointmentAsync(id);
    }

    public Task<AppointmentDetailsDto> getAppointmentByIdAsync(int id)
    {
        return _appointmentRepository.getAppointmentByIdAsync(id);
    }
}