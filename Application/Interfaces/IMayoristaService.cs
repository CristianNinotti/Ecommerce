using Application.Models.Request;
using Application.Models.Response;

namespace Application.Interfaces
{
    public interface IMayoristaService
    {
        List<MayoristaResponse> GetAllMayoristas();
        MayoristaResponse? GetMayoristaById(int id);
        void CreateMayorista(MayoristaRequest request);
        bool UpdateMayorista(int id, MayoristaRequest request);
        bool DeleteMayorista(int id);
    }
}
