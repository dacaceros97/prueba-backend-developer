public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string Role { get; set; } = "USER";
    public decimal Salary { get; set; }
    public DateTime DateContractor { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}