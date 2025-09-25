namespace ReactWithASP.Server.Services;

using Microsoft.EntityFrameworkCore;
using ReactWithASP.Server.Data;
using ReactWithASP.Server.Models.DTOs;
using ReactWithASP.Server.Models.Entities;

public class SaveSubjectService(AppDbContext context) : ISaveSubjectService
{
    public async Task Store(SubjectDto dto)
    {
        var subject = new Subject(dto.Title, dto.Credits);
        context.Subjects.Add(subject);
        await context.SaveChangesAsync();
    }

    public async Task Update(int id, SubjectDto dto)
    {
        var subject = await context.Subjects.FirstOrDefaultAsync(i => i.Id == id);
        if (subject != null)
        {
            subject.SetValues(dto.Title, dto.Credits);
            context.Subjects.Update(subject);
            await context.SaveChangesAsync();
        }
    }
    public async Task Delete(int id)
    {
        var subject = await context.Subjects.FirstOrDefaultAsync(i => i.Id == id);
        if (subject != null)
        {
            context.Subjects.Remove(subject);
            await context.SaveChangesAsync();
        }
    }
}

