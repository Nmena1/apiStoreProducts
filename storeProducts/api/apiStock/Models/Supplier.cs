using System;
using System.Collections.Generic;

namespace apiStock.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Descripcion { get; set; }

    public string? Contact { get; set; }

    public string? Phone { get; set; }

    public string? Cellphone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public int? Isactive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? ModifDate { get; set; }

    public string? ModifUser { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
