using ReactWithASP.Server.Models.DTOs;


namespace ReactWithASP.Server.Services;

    public interface IGetStudentService
    {
        Task<List<StudentDto>> GetAll();
        Task<StudentDto?> Get(int id);
}

