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

    public async Task<bool> TestConnectionAsync()
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return true;
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Błąd połączenia z bazą: {ex.Message}");
            return false;
        }
    }

    public async Task<List<string>> GetAllAsync()
    {

        List<string> result = new List<string>();
       // ExecuteReader() służy do wykonywania zapytań SELECT, które zwracają wiersze danych. Zwraca obiekt SqlDataReader, który pozwala iterować po wynikach wiersz po wierszu.

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        // Tworzymy komendę z zapytaniem SELECT
        using var command = new SqlCommand("SELECT * FROM Appointments", connection);

        // ExecuteReader zwraca SqlDataReader
        using var reader = await command.ExecuteReaderAsync();

        // Iterujemy po wynikach — Read() przesuwa "kursor" na następny wiersz
        while (await reader.ReadAsync())
        {
            // Odczytujemy wartości kolumn — przez indeks lub nazwę
            int id = reader.GetInt32(0);           // kolumna 0 = Id
            int firstName = reader.GetInt32(1); // kolumna 1 = FirstName
            int lastName = reader.GetInt32(2);  // kolumna 2 = LastName
      
            result.Add($"Student: {id} - {firstName} {lastName}");
        }

        return result;

    }   
    
}