using Microsoft.AspNetCore.Mvc;
using ReactWithASP.Server.Models.DTOs;
using ReactWithASP.Server.Services;
using Microsoft.AspNetCore.Authorization;


namespace ReactWithASP.Server.Controllers;

[ApiController]
[Route("api/subjects")]
[Authorize]

public class SubjectController(IGetSubjectService getSubjectService, ISaveSubjectService saveSubjectService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await getSubjectService.GetAll();
        return Ok(results);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSubject(int id)
    {
        var subject = await getSubjectService.Get(id);
        return Ok(subject);

    }

    [HttpPut(template: "{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Put(int id, SubjectDto dto)
    {
        await saveSubjectService.Update(id, dto);
        return Ok();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post(SubjectDto dto)
    {
        await saveSubjectService.Store(dto);
        return Ok();
    }
    [HttpDelete(template: "{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await saveSubjectService.Delete(id);
        return Ok();
    }
}


