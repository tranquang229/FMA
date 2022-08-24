using System.ComponentModel;

namespace FMA.Entities.Enum;

public enum EnumRole
{
    [Description("User")]
    User = 1,

    [Description("Admin")]
    Admin,

    [Description("SuperAdmin")]
    SuperAdmin,
}