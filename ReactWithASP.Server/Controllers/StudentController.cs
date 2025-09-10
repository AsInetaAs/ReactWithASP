using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactWithASP.Server.Data;
using ReactWithASP.Server.Models.DTOs;


namespace ReactWithASP.Server.Controllers;

[ApiController]
[Route("api/[controller]")]

public class StudentController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var students = await context.Students.ToListAsync();
        var results = new List<StudentDto>();

        foreach (var student in students)
        {
            results.Add(new StudentDto(student.Id, FullName:$"{student.FirstName} {student.LastName}", student.Email));
        }
        return Ok(results);
        }
}
