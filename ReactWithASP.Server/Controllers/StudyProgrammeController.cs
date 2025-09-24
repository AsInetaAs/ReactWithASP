using Microsoft.AspNetCore.Mvc;
using ReactWithASP.Server.Models.DTOs;
using ReactWithASP.Server.Services;


namespace ReactWithASP.Server.Controllers;

[ApiController]
[Route("api/programmes")]

public class StudyProgrammeController(IGetStudyProgrammeService getStudyProgrammeService, ISaveStudyProgrammeService saveStudyProgrammeService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await getStudyProgrammeService.GetAll();
        return Ok(results);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetStudyProgramme(int id)
    {
        var programme = await getStudyProgrammeService.Get(id);
        return Ok(programme);

    }

    [HttpPut(template: "{id:int}")]
    public async Task<IActionResult> Put(int id, StudyProgrammeDto dto)
    {
        await saveStudyProgrammeService.Update(id, dto);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post(StudyProgrammeDto dto)
    {
        await saveStudyProgrammeService.Store(dto);
        return Ok();
    }
}



