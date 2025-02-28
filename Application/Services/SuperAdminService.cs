using Application.Interfaces;
using Application.Mappings;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Interfaces;

namespace Application.Services
{
    public class SuperAdminService : ISuperAdminService
    {
        private readonly ISuperAdminRepository _superAdminRepository;

        public SuperAdminService(ISuperAdminRepository superAdminRepository)
        {
            _superAdminRepository = superAdminRepository;
        }

        public List<SuperAdminResponse> GetAllSuperAdmins()
        {
            try
            {
                var superAdmins = _superAdminRepository.GetAllSuperAdmins();
                return SuperAdminProfile.ToSuperAdminResponse(superAdmins);
            }
            catch (Exception e)
            {
                Console.WriteLine("Hay un error en la clase");
                throw new Exception(e.Message);
            }
        }

        public SuperAdminResponse? GetSuperAdminById(int id)
        {
            var superAdmin = _superAdminRepository.GetSuperAdminById(id);

            if (superAdmin != null)
            {
                return SuperAdminProfile.ToSuperAdminResponse(superAdmin);
            }

            return null;
        }

        public void CreateSuperAdmin(SuperAdminRequest entity)
        {
            var superAdminEntity = SuperAdminProfile.ToSuperAdminEntity(entity);
            _superAdminRepository.AddSuperAdmin(superAdminEntity);
        }

        public bool UpdateSuperAdmin(int id, SuperAdminRequest superAdmin)
        {
            var superAdminEntity = _superAdminRepository.GetSuperAdminById(id);

            if (superAdminEntity != null)
            {

                if (!string.IsNullOrEmpty(superAdmin.NameAccount) && superAdmin.NameAccount != "string")
                {
                    superAdminEntity.NameAccount = superAdmin.NameAccount;
                }

                if (!string.IsNullOrEmpty(superAdmin.Password) && superAdmin.Password != "string")
                {
                    superAdminEntity.Password = superAdmin.Password;
                }

                if (!string.IsNullOrEmpty(superAdmin.FirstName) && superAdmin.FirstName != "string")
                {
                    superAdminEntity.FirstName = superAdmin.FirstName;
                }

                if (!string.IsNullOrEmpty(superAdmin.LastName) && superAdmin.LastName != "string")
                {
                    superAdminEntity.LastName = superAdmin.LastName;
                }

                if (superAdmin.Dni != 0)
                {
                    superAdminEntity.Dni = superAdmin.Dni;
                }

                if (!string.IsNullOrEmpty(superAdmin.Email) && superAdmin.Email != "string")
                {
                    superAdminEntity.Email = superAdmin.Email;
                }
                _superAdminRepository.UpdateSuperAdmin(superAdminEntity);

                return true;
            }

            return false;
        }



        public bool DeleteSuperAdmin(int id)
        {
            var superAdmin = _superAdminRepository.GetSuperAdminById(id);

            if (superAdmin != null)
            {
                _superAdminRepository.DeleteSuperAdmin(superAdmin);

                return true;
            }

            return false;
        }
    }
}