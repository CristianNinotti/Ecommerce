using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int? Id { get; set; } = null;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string NameAccount { get; set; } = string.Empty;
        public string Password {  get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
 
        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }
}
