using Application.Models.Request;
using Application.Models.Response;
using DomainEntity = Domain.Entities;

namespace Application.Mappings
{
    public static class SuperAdminProfile
    {
        public static DomainEntity.SuperAdmin ToSuperAdminEntity(SuperAdminRequest request)
        {
            return new DomainEntity.SuperAdmin()
            {
                NameAccount = request.NameAccount,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dni = request.Dni,
                Email = request.Email,

            };
        }



        public static SuperAdminResponse ToSuperAdminResponse(DomainEntity.SuperAdmin superAdmin)
        {
            return new SuperAdminResponse()
            {
                NameAccount = superAdmin.NameAccount,
                Id = superAdmin.Id,
                FirstName = superAdmin.FirstName,
                LastName = superAdmin.LastName,
                Dni = superAdmin.Dni,
                Email = superAdmin.Email
            };
        }

        public static List<SuperAdminResponse> ToSuperAdminResponse(List<DomainEntity.SuperAdmin> superAdmins)
        {
            return superAdmins.Select(c => new SuperAdminResponse
            {
                NameAccount = c.NameAccount,
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Dni = c.Dni,
                Email = c.Email

            }).ToList();
        }
    }
}