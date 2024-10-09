using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProjectsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        var projects = await _context.Projects.Include(p => p.Departament).Include(p => p.User).ToListAsync();
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
        var project = await _context.Projects.Include(p => p.Departament).Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }

[HttpPost]
public async Task<IActionResult> CreateProject([FromBody] ProjectDto projectDto)
{
    if (projectDto == null)
    {
        return BadRequest("El objeto ProjectDto no puede ser nulo.");
    }

    if (string.IsNullOrWhiteSpace(projectDto.Name))
    {
        return BadRequest("El nombre del proyecto es obligatorio.");
    }

    var departament = await _context.Departaments.FindAsync(projectDto.DepartamentId);
    var user = await _context.Users.FindAsync(projectDto.UserId);

    if (departament == null || user == null)
    {
        return BadRequest("El departamento o el usuario no existen.");
    }

    var project = new Project
    {
        Name = projectDto.Name,
        DateStart = projectDto.DateStart ?? DateTime.Now,
        DateEnd = projectDto.DateEnd ?? DateTime.Now,
        DepartamentId = projectDto.DepartamentId,
        Departament = departament,
        UserId = user.Id,
        User = user
    };

    _context.Projects.Add(project);
    await _context.SaveChangesAsync();
    return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
}

[HttpPut("{id}")]
public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectDto projectDto)
{
    var project = await _context.Projects.FindAsync(id);
    if (project == null)
    {
        return NotFound();
    }

    if (projectDto.Name != null)
    {
        project.Name = projectDto.Name;
    }
    if (projectDto.DateStart.HasValue)
    {
        project.DateStart = projectDto.DateStart.Value;
    }
    if (projectDto.DateEnd.HasValue)
    {
        project.DateEnd = projectDto.DateEnd.Value;
    }
    if (projectDto.DepartamentId != 0)
    {
        project.DepartamentId = projectDto.DepartamentId;
    }

    _context.Entry(project).State = EntityState.Modified;
    await _context.SaveChangesAsync();

    return NoContent();
}

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
