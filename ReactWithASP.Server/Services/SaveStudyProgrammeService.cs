namespace ReactWithASP.Server.Services;

using Microsoft.EntityFrameworkCore;
using ReactWithASP.Server.Data;
using ReactWithASP.Server.Models.DTOs;
using ReactWithASP.Server.Models.Entities;

    public class SaveStudyProgrammeService(AppDbContext context) : ISaveStudyProgrammeService
{
    public async Task Store(StudyProgrammeDto dto)
    {
        var programme = new StudyProgramme(dto.Title, dto.Description, dto.Duration);
        context.StudyProgrammes.Add(programme);
        await context.SaveChangesAsync();
    }

    public async Task Update(int id, StudyProgrammeDto dto)
    {
        var programme = await context.StudyProgrammes.FirstOrDefaultAsync(i => i.Id == id);
        if (programme != null)
        {
            programme.SetValues(dto.Title, dto.Description, dto.Duration);
            context.StudyProgrammes.Update(programme);
            await context.SaveChangesAsync();
        }
    }
    public async Task Delete(int id)
    {
        var programme = await context.StudyProgrammes.FirstOrDefaultAsync(i => i.Id == id);
        if (programme != null)
        {
            context.StudyProgrammes.Remove(programme);
            await context.SaveChangesAsync();
        }
    }
    public async Task AddSubject(int programmeId, int subjectId)
    {
        var programme = await context.StudyProgrammes
            .Include(p => p.Subjects)
            .FirstOrDefaultAsync(p => p.Id == programmeId);
        var subject = await context.Subjects.FirstOrDefaultAsync(s => s.Id == subjectId);
        if (programme == null || subject == null) return;

        if (!programme.Subjects.Any(s => s.Id == subjectId))
            programme.Subjects.Add(subject);

        await context.SaveChangesAsync();
    
    }
    public async Task RemoveSubject(int programmeId, int subjectId)
    {
        var programme = await context.StudyProgrammes
            .Include(p => p.Subjects)
            .FirstOrDefaultAsync(p => p.Id == programmeId);
        if (programme == null) return;

        var subject = programme.Subjects.FirstOrDefault(s => s.Id == subjectId);
        if (subject == null) return;

        programme.Subjects.Remove(subject);
        await context.SaveChangesAsync();
    }
}
