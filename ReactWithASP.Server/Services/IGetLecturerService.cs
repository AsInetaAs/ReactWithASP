using ReactWithASP.Server.Models.DTOs;

namespace ReactWithASP.Server.Services
{
    public interface IGetLecturerService
    {
        Task<List<LecturerDto>> GetAll();
        Task<LecturerDto?> Get(int id);
    }
}
