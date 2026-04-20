using apbd_cwiczenie_6.Models;
namespace apbd_cwiczenie_6.Repositories;

public interface IAppointmentRepository
{
    Task<bool> TestConnectionAsync();
    Task<List<string>> GetAllAsync();
}