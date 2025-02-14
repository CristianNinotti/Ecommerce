using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMinoristaRepository
    {
        List<Minorista> GetAllMinoristas();
        Minorista? GetMinoristaById(int id);
        void CreateMinorista (Minorista minorista);
        void UpdateMinorista(Minorista minorista);
        void DeleteMinorista(Minorista minorista);

    }
}
