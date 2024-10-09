using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

[HttpPost]
public async Task<IActionResult> CreateUser(User user)
{
    var emailExists = await _context.Users.AnyAsync(u => u.Email == user.Email);
    
    if (emailExists)
    {
        return BadRequest("El correo electrónico ya está en uso.");
    }

    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
}

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto updatedUserDto)
    {
        var existingUser = await _context.Users.FindAsync(id);
        if (existingUser == null)
        {
            return NotFound("Usuario no encontrado.");
        }

        if (!string.IsNullOrEmpty(updatedUserDto.Name))
        {
            existingUser.Name = updatedUserDto.Name;
        }

        if (!string.IsNullOrEmpty(updatedUserDto.Email))
        {
            if (await _context.Users.AnyAsync(u => u.Email == updatedUserDto.Email && u.Id != id))
            {
                return BadRequest("El email ya está en uso.");
            }
            existingUser.Email = updatedUserDto.Email;
        }

        if (!string.IsNullOrEmpty(updatedUserDto.Password))
        {
            existingUser.Password = BCrypt.Net.BCrypt.HashPassword(updatedUserDto.Password);
        }

        if (updatedUserDto.Salary.HasValue)
        {
            existingUser.Salary = updatedUserDto.Salary.Value;
        }

        if (updatedUserDto.DateContractor.HasValue)
        {
            existingUser.DateContractor = updatedUserDto.DateContractor.Value;
        }

        if (updatedUserDto.DateOfBirth.HasValue)
        {
            existingUser.DateOfBirth = updatedUserDto.DateOfBirth.Value;
        }

        _context.Entry(existingUser).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok("Usuario actualizado con éxito.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
