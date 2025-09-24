using Microsoft.AspNetCore.Mvc;
using ReactWithASP.Server.Models.DTOs;
using ReactWithASP.Server.Services;

namespace ReactWithASP.Server.Controllers;

[ApiController]
[Route("api/lecturers")]

public class LecturerController(IGetLecturerService getLecturerService, ISaveLecturerService saveLecturerService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await getLecturerService.GetAll();
        return Ok(results);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetLecturer(int id)
    {
        var lecturer = await getLecturerService.Get(id);
        return Ok(lecturer);

    }

    [HttpPut(template: "{id:int}")]
    public async Task<IActionResult> Put(int id, LecturerDto dto)
    {
        await saveLecturerService.Update(id, dto);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post(LecturerDto dto)
    {
        await saveLecturerService.Store(dto);
        return Ok();
    }
}


