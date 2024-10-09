using System.Text.Json.Serialization;

public class Project
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public int DepartamentId { get; set; }
    
    [JsonIgnore]
    public  virtual Departament Departament { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
}