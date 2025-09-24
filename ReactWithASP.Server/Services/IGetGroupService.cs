using ReactWithASP.Server.Models.DTOs;


namespace ReactWithASP.Server.Services;

    public interface IGetGroupService
    {
    Task<List<GroupDto>> GetAll();
    Task<GroupDto?> Get(int id);
    }

