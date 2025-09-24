using Microsoft.EntityFrameworkCore;
using ReactWithASP.Server.Data;
using ReactWithASP.Server.Models.DTOs;
using ReactWithASP.Server.Models.Entities;

namespace ReactWithASP.Server.Services;

    public class GetStudyPragrammeService(AppDbContext context) : IGetStudyProgrammeService
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

    private StudyProgrammeDto MapDto(StudyProgramme programme) =>
        new StudyProgrammeDto(programme.Id, programme.Title, programme.Description, programme.Duration);
}



