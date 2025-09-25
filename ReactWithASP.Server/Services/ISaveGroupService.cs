namespace ReactWithASP.Server.Services;
using ReactWithASP.Server.Models.DTOs;

public interface ISaveGroupService
    {
    Task Store(GroupDto dto);

    Task Update(int id, GroupDto dto);
    Task Delete(int id);
}

