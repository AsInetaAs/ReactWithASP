using ReactWithASP.Server.Models.DTOs;

namespace ReactWithASP.Server.Services
{
    public interface ISaveLecturerService
    {
        Task Store(LecturerDto dto);

        Task Update(int id, LecturerDto dto);
    }
}
