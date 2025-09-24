using Microsoft.EntityFrameworkCore;
using ReactWithASP.Server.Data;
using ReactWithASP.Server.Models.DTOs;
using ReactWithASP.Server.Models.Entities;

namespace ReactWithASP.Server.Services;

public class GetGroupService(AppDbContext context) : IGetGroupService
{
    public async Task<List<GroupDto>> GetAll()
    {
        var groups = await context.Groups.ToListAsync();
        List<GroupDto> results = new List<GroupDto>();

        foreach (var group in groups)
        {
            results.Add(MapDto(group));
        }
        return results;
    }
    public async Task<GroupDto?> Get(int id)
    {
        var group = await context.Groups.FirstOrDefaultAsync(i => i.Id == id);
        if (group == null) return null;
        return MapDto(group);
    }

    private GroupDto MapDto(Group group) =>
        new GroupDto(group.Id, group.Title, group.Year);
}
