using ReactWithASP.Server.Models.DTOs;


namespace ReactWithASP.Server.Services;

    public interface IGetStudyProgrammeService
    {
    Task<List<StudyProgrammeDto>> GetAll();
    Task<StudyProgrammeDto?> Get(int id);

    Task<List<SubjectDto>> GetSubjects(int programmeId);
}

