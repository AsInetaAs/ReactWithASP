namespace ReactWithASP.Server.Services;
using ReactWithASP.Server.Models.DTOs;

public interface ISaveStudentService
    {
        Task Store(StudentDto dto);

       Task Update(int id, StudentDto dto);
}

