﻿using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;

namespace Application.Mappings
{
    public static class SuperAdminProfile
    {
        public static SuperAdmin ToSuperAdminEntity(SuperAdminRequest request)
        {
            return new SuperAdmin()
            {
                NameAccount = request.NameAccount,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dni = request.Dni,
                Email = request.Email,

            };
        }

        public static SuperAdminResponse ToSuperAdminResponse(SuperAdmin superAdmin)
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

        public static List<SuperAdminResponse> ToSuperAdminResponse(List<SuperAdmin> superAdmins)
        {
            return superAdmins.Select(c => new SuperAdminResponse
            {
                NameAccount = c.NameAccount,
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Dni = c.Dni,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address

            }).ToList();
        }

        public static void ToSuperAdminUpdate(SuperAdmin superAdmin, SuperAdminRequest request)
        {
            superAdmin.FirstName = request.FirstName;
            superAdmin.LastName = request.LastName;
            superAdmin.NameAccount = request.NameAccount;
            superAdmin.Password = request.Password;
            superAdmin.Email = request.Email;
            superAdmin.Dni = request.Dni;
            superAdmin.PhoneNumber = request.PhoneNumber;
            superAdmin.Address = request.Address;
        }
    }
}