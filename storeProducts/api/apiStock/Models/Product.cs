using System;
using System.Collections.Generic;

namespace apiStock.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Price { get; set; }

    public int? Stock { get; set; }

    public int? CategoryId { get; set; }

    public int? SupplierId { get; set; }

    public int? Isactive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? ModifDate { get; set; }

    public string? ModifUser { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();

    public virtual Supplier? Supplier { get; set; }
}
