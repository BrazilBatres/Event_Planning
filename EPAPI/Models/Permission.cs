using System;
using System.Collections.Generic;

namespace EPAPI.Models;

public partial class Permission
{
    public int Id { get; set; }

    public string? PermissionName { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; } = new List<RolePermission>();
}
