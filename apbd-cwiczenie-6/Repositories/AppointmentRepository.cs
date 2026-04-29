using apbd_cwiczenie_6.DTO;
using apbd_cwiczenie_6.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
namespace apbd_cwiczenie_6.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly string _connectionString;
    
    public AppointmentRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Lack of ConnectioString \"DefaultConnection\" in configuration.");
    }
    
    public async Task<List<AppointmentListDto>> getAllAppointmentsAsync()
    {
        var resultList = new List<AppointmentListDto>();
        using var conn = new SqlConnection(_connectionString);
        using var comm = new  SqlCommand("SELECT * FROM Appointments a JOIN Patients p ON p.IdPatient = a.IdPatient", conn);
        await conn.OpenAsync();
        await using var reader = await comm.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            resultList.Add(new AppointmentListDto()
            {
                IdAppointment = (int) reader["IdAppointment"],
                AppointmentDate =  (DateTime) reader["AppointmentDate"],
                Status = (string) reader["Status"],
                Reason = (string) reader["Reason"],
                PatientFullName = (string) reader["FirstName"] + "  " + reader["LastName"],
                PatientEmail = (string) reader["Email"]
            });
        }
        return resultList;
    }

    public async Task<bool> insertAppointmentAsync(CreateAppointmentRequestDto appointment)
    {
        using var conn = new SqlConnection(_connectionString);
        using var comm = new  SqlCommand("insert into appointments (IdPatient, IdDoctor, " +
                                         "AppointmentDate, Status, Reason, InternalNotes, CreatedAt) values (" +
                                         "@IdPatient, @IdDoctor,@AppointmentDate, @Status, @Reason, @InternalNotes, GETDATE())", conn);
        await conn.OpenAsync();

        comm.Parameters.AddWithValue("@IdPatient", appointment.IdPatient);
        comm.Parameters.AddWithValue("@IdDoctor", appointment.IdDoctor);
        comm.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
        comm.Parameters.AddWithValue("@Status", "Scheduled");
        comm.Parameters.AddWithValue("@Reason", appointment.Reason);
        comm.Parameters.AddWithValue("@InternalNotes", string.Empty);
        await comm.ExecuteNonQueryAsync();
        return true;
     
    }

    public async Task<bool> deleteAppointmentAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        using var comm = new  SqlCommand("DELETE FROM Appointments WHERE IdAppointment = @id", conn);
        comm.Parameters.AddWithValue("@id", id);
        
        await conn.OpenAsync();

        await comm.ExecuteNonQueryAsync();
        return true;
    }

    public Task<bool> updateAppointmentAsync(UpdateAppointmentRequestDto appointment, int id)
    {
        throw new NotImplementedException();
    }
}