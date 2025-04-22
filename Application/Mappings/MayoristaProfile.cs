using Application.Models.Request;
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
                CUIT = request.CUIT,
                Categoria = request.Caterogia,
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
                CUIT = response.CUIT,
                Categoria = response.Categoria,
                NameAccount = response.NameAccount,
                Password = response.Password,
                Dni = response.Dni,
                Email = response.Email,
                PhoneNumber = response.PhoneNumber,
                Address = response.Address,
                Available = response.Available
            };
        }

        public static List<MayoristaResponse> ToMayoristaResponse(List<Mayorista> mayorista)
        {
            return mayorista.Select(c => new MayoristaResponse
            {
                NameAccount = c.NameAccount,
                Password = c.Password,
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                CUIT = c.CUIT,
                Categoria = c.Categoria,
                Dni = c.Dni,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address,
                Available = c.Available

            }).ToList();
        }

        public static void ToMayoristaUpdate (Mayorista mayorista, MayoristaRequest request)
        {
            mayorista.FirstName = request.FirstName;
            mayorista.LastName = request.LastName;
            mayorista.CUIT = request.CUIT;
            mayorista.Categoria = request.Caterogia;
            mayorista.NameAccount = request.NameAccount;
            mayorista.Password = request.Password;
            mayorista.Email = request.Email;
            mayorista.Dni = request.Dni;
            mayorista.PhoneNumber = request.PhoneNumber;
            mayorista.Address = request.Address;
        }
    }
}
