using System;
using System.Collections.Generic;

namespace apiStock.Models;

public partial class StockMovement
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public string? Type { get; set; }

    public string? Descripcion { get; set; }

    public int? Cuantity { get; set; }

    public int? UserId { get; set; }

    public string? Description { get; set; }

    public int? Isactive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? ModifDate { get; set; }

    public string? ModifUser { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
