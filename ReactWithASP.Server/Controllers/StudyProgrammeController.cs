using Microsoft.AspNetCore.Mvc;
using ReactWithASP.Server.Models.DTOs;
using ReactWithASP.Server.Services;
using Microsoft.AspNetCore.Authorization;

namespace ReactWithASP.Server.Controllers;

[ApiController]
[Route("api/programmes")]
[Authorize]

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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Put(int id, StudyProgrammeDto dto)
    {
        await saveStudyProgrammeService.Update(id, dto);
        return Ok();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post(StudyProgrammeDto dto)
    {
        await saveStudyProgrammeService.Store(dto);
        return Ok();
    }
    [HttpDelete(template: "{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await saveStudyProgrammeService.Delete(id);
        return Ok();
    }


    [HttpPost(template: "{programmeId:int}/subjects/{subjectId:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddSubject(int programmeId, int subjectId)
    {
        await saveStudyProgrammeService.AddSubject(programmeId, subjectId);
        return Ok();
    }
    [HttpDelete(template: "{programmeId:int}/subjects/{subjectId:int}")]
    public async Task<IActionResult> RemoveSubject(int programmeId, int subjectId)
    {
        await saveStudyProgrammeService.RemoveSubject(programmeId, subjectId);
        return Ok();
    }

    [HttpGet("{programmeId:int}/subjects")]

    public async Task<IActionResult> GetSubjects(int programmeId)
    {
        var items = await getStudyProgrammeService.GetSubjects(programmeId);
        
        return Ok(items ?? new List<SubjectDto>());
    }

}



