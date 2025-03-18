using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserAvailableService : IUserAvailableService
    {
        private readonly IMinoristaRepository _minoristaRepository;
        private readonly IMayoristaRepository _mayoristaRepository;
        private readonly ISuperAdminRepository _superAdminRepository;

        public UserAvailableService(
            IMinoristaRepository minoristaRepository,
            IMayoristaRepository mayoristaRepository,
            ISuperAdminRepository superAdminRepository)
        {
            _minoristaRepository = minoristaRepository;
            _mayoristaRepository = mayoristaRepository;
            _superAdminRepository = superAdminRepository;
        }

        // Validacion si el usuario esta habiltiado
        public bool IsUserAvailable(int userId)
        {
            return (_minoristaRepository.GetMinoristaById(userId)?.Available == true) ||
                   (_mayoristaRepository.GetMayoristaById(userId)?.Available == true) ||
                   (_superAdminRepository.GetSuperAdminById(userId)?.Available == true);
        }

        // Validacion si existen los usuarios.
        public bool UserExists(string nameAccount, string email, int? userId = null)
        {
            var mayorista = _mayoristaRepository.GetAllMayoristas()
                .Any(x => (x.NameAccount == nameAccount || x.Email == email) && (userId == null || x.Id != userId));

            var minorista = _minoristaRepository.GetAllMinoristas()
                .Any(x => (x.NameAccount == nameAccount || x.Email == email) && (userId == null || x.Id != userId));

            var superAdmin = _superAdminRepository.GetAllSuperAdmins()
                .Any(x => (x.NameAccount == nameAccount || x.Email == email) && (userId == null || x.Id != userId));

            return mayorista || minorista || superAdmin;
        }
    }
}