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
                var superAdmins = _superAdminRepository.GetAllSuperAdmins();
                return SuperAdminProfile.ToSuperAdminResponse(superAdmins);
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
                if (!string.IsNullOrEmpty(superAdmin.NameAccount) && superAdmin.NameAccount != "string" && !string.IsNullOrEmpty(superAdmin.Password) && superAdmin.Password != "string" &&
                    !string.IsNullOrEmpty(superAdmin.Password) && superAdmin.Password != "string" && !string.IsNullOrEmpty(superAdmin.FirstName) && superAdmin.FirstName != "string" &&
                    !string.IsNullOrEmpty(superAdmin.LastName) && superAdmin.LastName != "string" && superAdmin.Dni != 0  && !string.IsNullOrEmpty(superAdmin.Email) && superAdmin.Email != "string")
                {
                    superAdminEntity.NameAccount = superAdmin.NameAccount;
                    superAdminEntity.Password = superAdmin.Password;
                    superAdminEntity.FirstName = superAdmin.FirstName;
                    superAdminEntity.LastName = superAdmin.LastName;
                    superAdminEntity.Dni = superAdmin.Dni;
                    superAdminEntity.Email = superAdmin.Email;
                    _superAdminRepository.UpdateSuperAdmin(superAdminEntity);
                    return true;
                }
            }
            return false;
        }

        public bool SoftDeleteSuperAdmin(int id)
        {
            var superAdmin = _superAdminRepository.GetSuperAdminById(id);
            if (superAdmin != null)
            {
                superAdmin.Available = false;
                _superAdminRepository.SoftDeleteSuperAdmin(superAdmin);
                return true;
            }
            return false;
        }

        public bool HardDeleteSuperAdmin(int id)
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