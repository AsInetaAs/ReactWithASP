namespace ReactWithASP.Server.Services;

using Microsoft.EntityFrameworkCore;
using ReactWithASP.Server.Data;
using ReactWithASP.Server.Models.DTOs;
using ReactWithASP.Server.Models.Entities;

public class SaveStudentService(AppDbContext context) : ISaveStudentService
{
    public async Task Store(StudentDto dto)
    {
        var student = new Student(dto.FirstName, dto.LastName, dto.Email);
        context.Students.Add(student);
        await context.SaveChangesAsync();
    }

    public async Task Update(int id, StudentDto dto)
    {
        var student = await context.Students.FirstOrDefaultAsync(i => i.Id == id);
        if (student != null)
        {
            student.SetValues(dto.FirstName, dto.LastName, dto.Email);
            context.Students.Update(student);
            await context.SaveChangesAsync();
        }
    }
}

