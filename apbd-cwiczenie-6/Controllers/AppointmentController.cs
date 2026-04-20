using Microsoft.AspNetCore.Mvc;
using apbd_cwiczenie_6.DTO;
using apbd_cwiczenie_6.Models;
using apbd_cwiczenie_6.Repositories;

namespace apbd_cwiczenie_6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private IAppointmentRepository _appointmentRepository;

    
    public AppointmentController(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }
    
    [HttpGet("test")]
    public async Task<ActionResult<bool>> TestConnectionAsync()
    {
        Task<bool> feedback = _appointmentRepository.TestConnectionAsync();
        return Ok(feedback.Result);
    }

    [HttpGet]
    public async Task<ActionResult<List<string>>> GetAllAsync()
    {


        Task<List<string>> res = _appointmentRepository.GetAllAsync();
            return Ok(res.Result);

    } 
    
}