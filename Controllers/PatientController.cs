using Cwiczenia9.DTOs;
using Cwiczenia9.Exceptions;
using Cwiczenia9.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia9.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientsController(IDbService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails([FromRoute] int id)
    {
        try
        {
            var patientDetails = await service.GetPatientDetailsAsync(id);
            return Ok(patientDetails);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}