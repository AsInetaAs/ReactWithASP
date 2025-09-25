namespace ReactWithASP.Server.Services;
using ReactWithASP.Server.Models.DTOs;

public interface ISaveSubjectService
    {
    Task Store(SubjectDto dto);

    Task Update(int id, SubjectDto dto);
    Task Delete(int id);
}

