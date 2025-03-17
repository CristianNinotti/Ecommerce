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

        public bool IsUserAvailable(int userId)
        {
            return (_minoristaRepository.GetMinoristaById(userId)?.Available == true) ||
                   (_mayoristaRepository.GetMayoristaById(userId)?.Available == true) ||
                   (_superAdminRepository.GetSuperAdminById(userId)?.Available == true);
        }
    }
}