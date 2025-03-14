namespace apiStock.DTO
{
    public class productListDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Descripcion { get; set; }

        public decimal? Price { get; set; }

        public int? Stock { get; set; }

        public int? CategoryId { get; set; }

        public int? SupplierId { get; set; }

        public int? Isactive { get; set; }

        public string? supplierName { get; set; }

        public string? categoryName { get; set; }
    }
}
