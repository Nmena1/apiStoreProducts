namespace apiStock.DTO
{
    public class userDTO
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
    }
}
