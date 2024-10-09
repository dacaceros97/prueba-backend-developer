using System.Text.Json.Serialization;

public class Departament
{
    public int Id { get; set; }
    public required string Name { get; set; }
    [JsonIgnore]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}