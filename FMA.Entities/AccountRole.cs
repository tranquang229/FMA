using System.ComponentModel.DataAnnotations.Schema;

namespace FMA.Entities;

[Table("AccountRoles")]
public class AccountRole
{
    public long AccountId { get; set; }
    public long RoleId { get; set; }
}