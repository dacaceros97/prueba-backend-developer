using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DepartmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public DepartmentsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetDepartments()
    {
        var departments = await _context.Departaments.ToListAsync();
        return Ok(departments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepartment(int id)
    {
        var department = await _context.Departaments.FindAsync(id);
        if (department == null)
        {
            return NotFound();
        }
        return Ok(department);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartment(Departament department)
    {
        var nameExists = await _context.Departaments.AnyAsync(d => d.Name == department.Name);
        if (nameExists)
        {
            return BadRequest("El nombre del departamento ya está en uso.");
        }

        if (department == null)
        {
            return BadRequest("El departamento no puede ser nulo.");
        }

        _context.Departaments.Add(department);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department);
    }

[HttpPut("{id}")]
public async Task<IActionResult> UpdateDepartment(int id, Departament department)
{
    if (department == null)
    {
        return BadRequest("El departamento no puede ser nulo.");
    }

    var existingDepartment = await _context.Departaments.FindAsync(id);
    if (existingDepartment == null)
    {
        return NotFound("El departamento no fue encontrado.");
    }

    var nameExists = await _context.Departaments.AnyAsync(d => d.Name == department.Name && d.Id != id);
    if (nameExists)
    {
        return BadRequest("Otro departamento ya tiene el mismo nombre.");
    }

    existingDepartment.Name = department.Name;

    _context.Entry(existingDepartment).State = EntityState.Modified;
    await _context.SaveChangesAsync();

    return NoContent();
}


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var department = await _context.Departaments.FindAsync(id);
        if (department == null)
        {
            return NotFound();
        }

        _context.Departaments.Remove(department);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
