using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Response
{
    public class SuperAdminResponse
    {
        public int Id { get; set; }
        public string NameAccount { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Dni { get; set; }
        public string Email { get; set; } = string.Empty;

    }
}