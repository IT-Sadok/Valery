using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;
using WebApplication1.Models.Enums;

[Table(nameof(Account))]
public class Account
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Description { get; set; }
    public int? Age { get; set; }
    public ProgrammerLevel Level { get; set; }
    public TechStack Stack { get; set; }
}