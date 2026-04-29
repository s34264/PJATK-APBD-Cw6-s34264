namespace apbd_cwiczenie_6.DTO;

public class UpdateAppointmentRequestDto
{
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Status {get; set;} = string.Empty;
    public string Reason { get; set; } = string.Empty;
}