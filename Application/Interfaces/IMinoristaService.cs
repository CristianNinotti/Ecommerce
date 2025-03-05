using Application.Models.Response;
using Application.Models.Request;


namespace Application.Interfaces
{
    public interface IMinoristaService
    {
    List<MinoristaResponse> GetAllMinorista();
    MinoristaResponse? GetMinoristaById(int id);
    MinoristaResponse CreateMinorista(MinoristaRequest minorista); // Void - Sustituir Minorista response
    MinoristaResponse UpdateMinorista(int id, MinoristaRequest minorista); //Bool - Sustituir Minorista response
    MinoristaResponse DeleteMinorista(int id); //Bool - Sustituir Minorista response

    }
}
