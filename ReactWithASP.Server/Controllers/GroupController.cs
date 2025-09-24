using Microsoft.AspNetCore.Mvc;
using ReactWithASP.Server.Models.DTOs;
using ReactWithASP.Server.Services;


namespace ReactWithASP.Server.Controllers;

[ApiController]
[Route("api/groups")]
public class GroupController(IGetGroupService getGroupService, ISaveGroupService saveGroupService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await getGroupService.GetAll();
        return Ok(results);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetGroup(int id)
    {
        var group = await getGroupService.Get(id);
        return Ok(group);

    }

    [HttpPut(template: "{id:int}")]
    public async Task<IActionResult> Put(int id, GroupDto dto)
    {
        await saveGroupService.Update(id, dto);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post(GroupDto dto)
    {
        await saveGroupService.Store(dto);
        return Ok();
    }
}


