using System;
using System.Collections.Generic;

namespace EPAPI.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; } = new List<RolePermission>();

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
