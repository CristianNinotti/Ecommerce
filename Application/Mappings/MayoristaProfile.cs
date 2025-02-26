using Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Models.Response;

namespace Application.Mappings
{
    public static class MayoristaProfile
    {
        public static Mayorista ToMayoristaEntity(MayoristaRequest request)
        {
            return new Mayorista
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                NameAccount = request.NameAccount,
                Password = request.Password,
                Dni = request.Dni,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
            };

        }

        public static MayoristaResponse ToMayoristaResponse(Mayorista response)
        {   
            if (response == null)
                {
                throw new ArgumentNullException("El mayorista no puede ser nulo");
                }

            return new MayoristaResponse
            { 
                Id = response.Id,
                FirstName = response.FirstName,
                LastName = response.LastName,
                NameAccount = response.NameAccount,
                Password = response.Password,
                Dni = response.Dni,
                Email = response.Email,
                PhoneNumber = response.PhoneNumber,
                Address = response.Address,
            };
        }

        public static void ToMayoristaUpdate (Mayorista mayorista, MayoristaRequest request)
        {
            mayorista.FirstName = request.FirstName;
            mayorista.LastName = request.LastName;
            mayorista.NameAccount = request.NameAccount;
            mayorista.Password = request.Password;
            mayorista.Email = request.Email;
            mayorista.Dni = request.Dni;
            mayorista.PhoneNumber = request.PhoneNumber;
            mayorista.Address = request.Address;
        }
    }
}
