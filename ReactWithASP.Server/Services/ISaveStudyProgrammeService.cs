namespace ReactWithASP.Server.Services;
using ReactWithASP.Server.Models.DTOs;

    public interface ISaveStudyProgrammeService
    {
    Task Store(StudyProgrammeDto dto);

    Task Update(int id, StudyProgrammeDto dto);
    Task Delete(int id);
}

