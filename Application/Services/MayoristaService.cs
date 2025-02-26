using Application.Interfaces;
using Domain.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Response;
using Application.Mappings;
using Application.Models.Request;

namespace Application.Services
{
    public class MayoristaService : IMayoristaService
    {
        private readonly IMayoristaRepository _mayoristaRepository;

        public MayoristaService (IMayoristaRepository mayoristaRepository)
        {
            _mayoristaRepository = mayoristaRepository;
        }

        public List<Mayorista> GetAllMayoristas()
        {
            var mayoristas = _mayoristaRepository.GetMayoristas();
            return mayoristas.ToList();
        }

        public MayoristaResponse GetMayoristaById(int id)
        {
            var mayorista = _mayoristaRepository.GetMayoristaById (id);
            return MayoristaProfile.ToMayoristaResponse(mayorista);
        }

        public MayoristaResponse CreateMayorista(MayoristaRequest mayorista)
        {
            var MayoristaEntity = MayoristaProfile.ToMayoristaEntity(mayorista);
            _mayoristaRepository.CreateMayorista(MayoristaEntity);
            return MayoristaProfile.ToMayoristaResponse(MayoristaEntity);
        }

        public MayoristaResponse UpdateMayorista(int id, MayoristaRequest mayorista)
        {
            var MayoristaEntity = _mayoristaRepository.GetMayoristaById(id);
            if (MayoristaEntity == null)
            {
                throw new Exception("Mayorista no encontrado");
            }
            MayoristaProfile.ToMayoristaUpdate(MayoristaEntity, mayorista);
            _mayoristaRepository.UpdateMayorista(MayoristaEntity);
            return MayoristaProfile.ToMayoristaResponse(MayoristaEntity);
        }

        public MayoristaResponse DeleteMayorista(int id)
        {
            var MayoristaEntity = _mayoristaRepository.GetMayoristaById(id);
            if (MayoristaEntity == null)
            {
                throw new Exception("Mayorista no encontrado");
            }
            _mayoristaRepository.DeleteMayorista(MayoristaEntity);
            return MayoristaProfile.ToMayoristaResponse(MayoristaEntity);
        }
    }
}
