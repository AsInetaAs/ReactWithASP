using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactWithASP.Server.Data;
using ReactWithASP.Server.Models.DTOs;
using System.Collections.Generic;


namespace ReactWithASP.Server.Controllers;

[ApiController]
[Route("api/students")]

public class StudentController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await context.Students.ToListAsync();
        List<StudentDto> results = new List<StudentDto>();

        foreach (var student in students)
        {
            results.Add(new StudentDto(student.Id, student.FirstName, student.LastName, student.Email));
        }
        return Ok(results);
        }


    [HttpPut(template:"{id:int}")]
    public async Task<IActionResult> Put(int id, StudentDto dto)
    {
        var student = await context.Students.FirstOrDefaultAsync(i => i.Id == id);
        if (student != null)
        {
            student.SetValues(dto.FirstName, dto.LastName, dto.Email);
            context.Students.Update(student);
            await context.SaveChangesAsync();
        }
        return Ok();
    }

}


