namespace apbd_cwiczenie_6.DTO;

public class AppointmentDetailsDto
{
    public int IdAppointment { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; }
    public string Reason { get; set; }
    public string InternalNotes { get; set; }
    public DateTime CreatedAt { get; set; }
}