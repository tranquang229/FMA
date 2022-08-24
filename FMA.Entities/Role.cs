using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMA.Entities;

[Table("Roles")]
public class Role
{
    [Key]
    public long Id { get; set; }
    public string  Name { get; set; }
}