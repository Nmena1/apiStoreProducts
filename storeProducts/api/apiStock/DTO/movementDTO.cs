﻿namespace apiStock.DTO
{
    public class movementDTO
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public string? Type { get; set; }

        public string? Descripcion { get; set; }

        public int? Cuantity { get; set; }

        public int? UserId { get; set; }

        public int? Isactive { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateUser { get; set; }

        public DateTime? ModifDate { get; set; }

        public string? ModifUser { get; set; }
    }
}
