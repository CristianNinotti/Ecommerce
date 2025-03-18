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
        private readonly IUserAvailableService _userAvailableService;

        public MayoristaService (IMayoristaRepository mayoristaRepository, IUserAvailableService userAvailableService)
        {
            _mayoristaRepository = mayoristaRepository;
            _userAvailableService = userAvailableService;
        }

        public List<MayoristaResponse> GetAllMayoristas()
        {
            var mayoristas = _mayoristaRepository.GetAllMayoristas();
            return MayoristaProfile.ToMayoristaResponse(mayoristas);
        }

        public MayoristaResponse? GetMayoristaById(int id)
        {
            var mayorista = _mayoristaRepository.GetMayoristaById (id);
            if (mayorista != null)
            {
                return MayoristaProfile.ToMayoristaResponse(mayorista);
            }
            return null;
        }

        public void CreateMayorista(MayoristaRequest mayorista)
        {
            if (_userAvailableService.UserExists(mayorista.NameAccount, mayorista.Email))
            {
                throw new InvalidOperationException("El NameAccount o Email ya están en uso.");
            }

            var mayoristaEntity = MayoristaProfile.ToMayoristaEntity(mayorista);
            _mayoristaRepository.CreateMayorista(mayoristaEntity);
        }


        public bool UpdateMayorista(int id, MayoristaRequest mayorista)
        {
            var mayoristaEntity = _mayoristaRepository.GetMayoristaById(id);
            if (mayoristaEntity == null)
            {
                return false;  // No se encontró el mayorista, no se puede actualizar
            }

            // Validar que el NameAccount o Email no estén en uso por otro usuario
            if (_userAvailableService.UserExists(mayorista.NameAccount, mayorista.Email, id))
            {
                throw new InvalidOperationException("El NameAccount o Email ya están en uso por otro usuario.");
            }

            MayoristaProfile.ToMayoristaUpdate(mayoristaEntity, mayorista);
            _mayoristaRepository.UpdateMayorista(mayoristaEntity);

            return true;  // La actualización fue exitosa
        }


        public bool SoftDeleteMayorista(int id)
        {
            var MayoristaEntity = _mayoristaRepository.GetMayoristaById(id);
            if (MayoristaEntity == null)
            {
                return false;
            }
            MayoristaEntity.Available = false;
            _mayoristaRepository.SoftDeleteMayorista(MayoristaEntity);
            return true;
        }

        public bool HardDeleteMayorista(int id)
        {
            var MayoristaEntity = _mayoristaRepository.GetMayoristaById(id);
            if (MayoristaEntity == null)
            {
                return false;
            }
            _mayoristaRepository.DeleteMayorista(MayoristaEntity);
            return true;
        }
    }
}
