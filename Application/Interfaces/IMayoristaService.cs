using Application.Models.Request;
using Application.Models.Response;

namespace Application.Interfaces
{
    public interface IMayoristaService
    {
        List<MayoristaResponse> GetAllMayoristas();
        MayoristaResponse? GetMayoristaById(int id);
        MayoristaResponse CreateMayorista(MayoristaRequest request);
        MayoristaResponse UpdateMayorista(int id, MayoristaRequest request);
        MayoristaResponse DeleteMayorista(int id);
    }
}
