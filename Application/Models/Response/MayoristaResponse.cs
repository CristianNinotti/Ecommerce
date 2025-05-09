﻿namespace Application.Models.Response
{
    public class MayoristaResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? CUIT { get; set; }
        public string? Categoria {  get; set; }
        public string NameAccount { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Dni { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool Available { get; set; }
    }
}
