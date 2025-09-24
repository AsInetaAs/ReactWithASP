namespace ReactWithASP.Server.Services;

using Microsoft.EntityFrameworkCore;
using ReactWithASP.Server.Data;
using ReactWithASP.Server.Models.DTOs;
using ReactWithASP.Server.Models.Entities;

public class SaveGroupService(AppDbContext context) : ISaveGroupService
{
    public async Task Store(GroupDto dto)
    {
        var group = new Group(dto.Title, dto.Year);
        context.Groups.Add(group);
        await context.SaveChangesAsync();
    }

    public async Task Update(int id, GroupDto dto)
    {
        var group = await context.Groups.FirstOrDefaultAsync(i => i.Id == id);
        if (group != null)
        {
            group.SetValues(dto.Title, dto.Year);
            context.Groups.Update(group);
            await context.SaveChangesAsync();
        }
    }
}

