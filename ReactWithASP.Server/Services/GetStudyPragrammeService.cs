using Microsoft.EntityFrameworkCore;
using ReactWithASP.Server.Data;
using ReactWithASP.Server.Models.DTOs;
using ReactWithASP.Server.Models.Entities;

namespace ReactWithASP.Server.Services;

    public class GetStudyProgrammeService(AppDbContext context) : IGetStudyProgrammeService
{
    public async Task<List<StudyProgrammeDto>> GetAll()
    {
        var programmes = await context.StudyProgrammes.ToListAsync();
        List<StudyProgrammeDto> results = [];

        foreach (var programme in programmes)
        {
            results.Add(MapDto(programme));
        }
        return results;
    }
    public async Task<StudyProgrammeDto?> Get(int id)
    {
        var programme = await context.StudyProgrammes.FirstOrDefaultAsync(i => i.Id == id);
        if (programme == null) return null;
        return MapDto(programme);
    }
    public async Task<List<SubjectDto>> GetSubjects(int programmeId)
    {
        var programme = await context.StudyProgrammes
            .Include(p => p.Subjects)
            .FirstOrDefaultAsync(p => p.Id == programmeId);

        if (programme == null) return new List<SubjectDto>();

        return programme.Subjects
            .Select(s => new SubjectDto(s.Id, s.Title, s.Credits))
            .ToList();
    }


    private StudyProgrammeDto MapDto(StudyProgramme programme) =>
        new StudyProgrammeDto(programme.Id, programme.Title, programme.Description, programme.Duration);
}



