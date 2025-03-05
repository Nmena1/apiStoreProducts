using System;
using System.Collections.Generic;

namespace apiStock.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public int? RolId { get; set; }

    public int? Isactive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? ModifDate { get; set; }

    public string? ModifUser { get; set; }

    public virtual Role? Rol { get; set; }

    public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();

    public virtual ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();
}
