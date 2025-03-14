using Microsoft.Identity.Client;

namespace apiStock.DTO
{
    public class productDTO
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
    }
}
