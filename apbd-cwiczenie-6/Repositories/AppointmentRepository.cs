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
    
    public async Task<List<AppointmentListDto>> getAllAppointmentsAsync(string? status = null, string? patientLastName = null)
    {
        var resultList = new List<AppointmentListDto>();
        using var conn = new SqlConnection(_connectionString);
        using var comm = new  SqlCommand("SELECT " +
                                         "a.IdAppointment, " +
                                         "a.AppointmentDate, " +
                                         "a.Status, " +
                                         "a.Reason, " +
                                         "p.FirstName + N' ' + p.LastName AS PatientFullName, " +
                                         "p.Email AS PatientEmail " +
                                         "FROM dbo.Appointments a " +
                                         "JOIN dbo.Patients p ON p.IdPatient = a.IdPatient " +
                                         "WHERE (@Status IS NULL OR a.Status = @Status) " +
                                         "AND (@PatientLastName IS NULL OR p.LastName = @PatientLastName) " +
                                         "ORDER BY a.AppointmentDate", conn);
        comm.Parameters.AddWithValue("@Status", status);
        comm.Parameters.AddWithValue("@PatientLastName", patientLastName);
        
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
                PatientFullName = (string) reader["PatientFullName"],
                PatientEmail = (string) reader["PatientEmail"]
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

    public async Task<bool> updateAppointmentAsync(UpdateAppointmentRequestDto appointment, int id)
    {
        using var conn = new SqlConnection(_connectionString);
        using var comm = new  SqlCommand("UPDATE Appointments SET IdPatient = @IdPatient, IdDoctor = @IdDoctor, AppointmentDate = @AppointmentDate, Status = @Status, Reason = @Reason WHERE IdAppointment = @id", conn);
        
        comm.Parameters.AddWithValue("@IdPatient", appointment.IdPatient);
        comm.Parameters.AddWithValue("@IdDoctor", appointment.IdDoctor);
        comm.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
        comm.Parameters.AddWithValue("@Status", appointment.Status);
        comm.Parameters.AddWithValue("@Reason", appointment.Reason);
        comm.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        await comm.ExecuteNonQueryAsync();
        return true;

        /*
         *     public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Status {get; set;} = string.Empty;
    public string Reason { get; set; } = string.Empty;
         */

    }
}