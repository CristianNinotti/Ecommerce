using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMayoristaRepository
    {
        List<Mayorista> GetMayoristas();
        Mayorista? GetMayoristaById(int id);
        void CreateMayorista(Mayorista mayorista);
        void UpdateMayorista(Mayorista mayorista);
        void SoftDeleteMayorista(Mayorista mayorista);
        void DeleteMayorista(Mayorista mayorista);
    }
}
