using ReactWithASP.Server.Models.DTOs;


namespace ReactWithASP.Server.Services;

public interface IGetSubjectService
    {
    Task<List<SubjectDto>> GetAll();
    Task<SubjectDto?> Get(int id);
}

