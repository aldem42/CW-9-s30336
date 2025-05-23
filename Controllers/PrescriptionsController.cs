using Cwiczenia9.DTOs;
using Cwiczenia9.Exceptions;
using Cwiczenia9.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia9.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionsController(IDbService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] AddPrescriptionDto dto)
    {
        try
        {
            await service.AddPrescriptionAsync(dto);
            return Created("", null);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}