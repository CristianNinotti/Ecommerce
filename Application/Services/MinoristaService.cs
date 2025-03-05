using Application.Interfaces;
using Application.Mappings;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class MinoristaService : IMinoristaService
    {
        private readonly IMinoristaRepository _minoristaRepository;

        public MinoristaService (IMinoristaRepository minoristaRepository)
        {
            _minoristaRepository = minoristaRepository;
        }
        public List<MinoristaResponse> GetAllMinorista()
        {
            var minoristas = _minoristaRepository.GetAllMinoristas();
            return minoristas.Select(MinoristaProfile.ToMinoristaResponse).ToList();
        }

        public MinoristaResponse? GetMinoristaById(int id)
        {
            var minorista = _minoristaRepository.GetMinoristaById(id);
            if (minorista != null)
            {
                return MinoristaProfile.ToMinoristaResponse(minorista);
            }
            return null;
        }

        public MinoristaResponse CreateMinorista(MinoristaRequest minorista) // Sustituir Minorista Response - Void
        {
            var minoristaEntity = MinoristaProfile.ToMinoristaEntity(minorista);
            _minoristaRepository.CreateMinorista(minoristaEntity);
            return MinoristaProfile.ToMinoristaResponse(minoristaEntity);
        }

        public MinoristaResponse UpdateMinorista(int id, MinoristaRequest minorista) // Sustituir Minorista Response - Bool
        {
            var minoristaEntity = _minoristaRepository.GetMinoristaById(id);
            if (minoristaEntity == null) 
            {
                throw new Exception("Minorista no encontrado");
            }
            MinoristaProfile.ToMinoristaUpdate(minoristaEntity, minorista);
            _minoristaRepository.UpdateMinorista(minoristaEntity);
            return MinoristaProfile.ToMinoristaResponse(minoristaEntity);
        }

        public MinoristaResponse DeleteMinorista(int id)                        // Sustituir Minorista Response - Bool
        {
            var minoristaEntity = _minoristaRepository.GetMinoristaById(id);
            if ( minoristaEntity == null )
            {
                throw new Exception("Minorista no encontrado");
            }
            _minoristaRepository.DeleteMinorista(minoristaEntity);
            return MinoristaProfile.ToMinoristaResponse(minoristaEntity);
        }
    }
}
