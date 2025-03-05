using System;
using System.Collections.Generic;

namespace apiStock.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Isactive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? ModifDate { get; set; }

    public string? ModifUser { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
