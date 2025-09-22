using Microsoft.AspNetCore.Mvc;
using ReactWithASP.Server.Models.DTOs;
using ReactWithASP.Server.Services;


namespace ReactWithASP.Server.Controllers;

[ApiController]
[Route("api/students")]

public class StudentController(IGetStudentService getStudentService, ISaveStudentService saveStudentService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await getStudentService.GetAll();
        return Ok(results);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetStudent(int id)
    {
        var student = await getStudentService.Get(id);
        return Ok(student);

    }

    [HttpPut(template:"{id:int}")]
    public async Task<IActionResult> Put(int id, StudentDto dto)
    {
       await saveStudentService.Update(id, dto);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post(StudentDto dto)
    {
        await saveStudentService.Store(dto);
        return Ok();
    }
}


