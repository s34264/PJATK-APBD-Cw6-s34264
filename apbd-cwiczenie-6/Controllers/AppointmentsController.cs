using Microsoft.AspNetCore.Mvc;
using apbd_cwiczenie_6.DTO;
using apbd_cwiczenie_6.Models;
using apbd_cwiczenie_6.Repositories;
using apbd_cwiczenie_6.Services;
using Microsoft.Data.SqlClient;

namespace apbd_cwiczenie_6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private IAppointmentService _appointmentService;

    
    public AppointmentsController(IAppointmentService appointmenService)
    {
        _appointmentService = appointmenService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(string? status = null, string? patientLastName = null)
    {
        return Ok(await _appointmentService.getAllAppointments(status, patientLastName));
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(await _appointmentService.getAppointmentByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult> PostAppointment([FromBody] CreateAppointmentRequestDto appointment)
    {
        try
        {
            await _appointmentService.addAppointmentAsync(new CreateAppointmentRequestDto()
            {
                IdPatient = appointment.IdPatient,
                IdDoctor = appointment.IdDoctor,
                Reason = appointment.Reason,
                AppointmentDate = appointment.AppointmentDate
            });
            return Created();
        }
        catch(SqlException ex)
        {
           return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAppointment([FromBody] UpdateAppointmentRequestDto appointment, int id)
    {
        return Ok(await _appointmentService.updateAppointmentAsync(appointment, id));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAllAppointment(int id)
    {
        try
        {
            await _appointmentService.deleteAppointmentAsync(id);
            return NoContent();
        }
        catch (SqlException ex)
        {
            return BadRequest(ex.Message);
        }
       
    }
    
}