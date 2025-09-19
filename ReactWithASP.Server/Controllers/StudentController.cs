using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactWithASP.Server.Data;
using ReactWithASP.Server.Models.DTOs;
using ReactWithASP.Server.Services;
using System.Collections.Generic;


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


    [HttpPut(template:"{id:int}")]
    public async Task<IActionResult> Put(int id, StudentDto dto)
    {
       await saveStudentService.Update(id, dto);
        return Ok();
    }

}


