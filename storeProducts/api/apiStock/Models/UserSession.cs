using System;
using System.Collections.Generic;

namespace apiStock.Models;

public partial class UserSession
{
    public Guid SessionId { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; } = null!;

    public string? Ipaddress { get; set; }

    public string? UserAgent { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? Isactive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? ModifDate { get; set; }

    public string? ModifUser { get; set; }

    public virtual User User { get; set; } = null!;
}
