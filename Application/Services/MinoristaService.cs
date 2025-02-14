using Application.Interfaces;
using Application.Mappings;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MinoristaService : IMinoristaService
    {
        private readonly IMinoristaRepository _minoristaRepository;

        public MinoristaService (IMinoristaRepository minoristaRepository)
        {
            _minoristaRepository = minoristaRepository;
        }
        public List<Minorista> GetAllMinorista()
        {
            var minoristas = _minoristaRepository.GetAllMinoristas();
            return minoristas.ToList();
        }

        public MinoristaResponse GetMinoristaById(int id)
        {
            var minorista = _minoristaRepository.GetMinoristaById(id);
            return MinoristaProfile.ToMinoristaResponse(minorista);
        }

        public MinoristaResponse CreateMinorista(MinoristaRequest minorista)
        {
            var minoristaEntity = MinoristaProfile.ToMinoristaEntity(minorista);
            _minoristaRepository.CreateMinorista(minoristaEntity);
            return MinoristaProfile.ToMinoristaResponse(minoristaEntity);
        }

        public MinoristaResponse UpdateMinorista(int id, MinoristaRequest minorista)
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

        public MinoristaResponse DeleteMinorista(int id)
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
