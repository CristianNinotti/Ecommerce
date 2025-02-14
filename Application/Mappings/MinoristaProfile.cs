using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;

namespace Application.Mappings
{
   public static class MinoristaProfile
    {
        public static Minorista ToMinoristaEntity(MinoristaRequest minorista)
        {
            return new Minorista
            {
                FirstName = minorista.FirstName,
                LastName = minorista.LastName,
                NameAccount = minorista.NameAccount,
                Password = minorista.Password,
                Email = minorista.Email,
                Dni = minorista.Dni,
                PhoneNumber = minorista.PhoneNumber,
                Address = minorista.Address,
            };
        }

        public static MinoristaResponse ToMinoristaResponse(Minorista minorista)
        {
            if (minorista == null)
            {
                throw new ArgumentNullException(nameof(minorista), "El minorista no puede ser nulo.");
            }
            return new MinoristaResponse
            {
                Id = minorista.Id,
                FirstName = minorista.FirstName,
                LastName = minorista.LastName,
                NameAccount = minorista.NameAccount,
                Password = minorista.Password,
                Email = minorista.Email,
                Dni = minorista.Dni,
                PhoneNumber = minorista.PhoneNumber,
                Address = minorista.Address,
            };
        }

        public static void ToMinoristaUpdate (Minorista minorista, MinoristaRequest request)
        {
            minorista.FirstName = request.FirstName;
            minorista.LastName = request.LastName;
            minorista.NameAccount = request.NameAccount;
            minorista.Password = request.Password;
            minorista.Email = request.Email;
            minorista.Dni = request.Dni;
            minorista.PhoneNumber = request.PhoneNumber;
            minorista.Address = request.Address;
        }
    }
    
    
}
