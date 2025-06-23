using ManagementApi.DTOs;
using ManagementApi.Entities;
using ManagementApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementApi.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(UserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await userService.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await userService.AddAsync(dto);
        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] User user)
    {
        if (id != user.Id) return BadRequest();

        var existingUser = await userService.GetByIdAsync(id);
        if (existingUser == null) return NotFound();

        await userService.UpdateAsync(user);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await userService.DeleteAsync(id);
        return NoContent();
    }
}