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
}
