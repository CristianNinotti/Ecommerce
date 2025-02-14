using Application.Models.Response;
using Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Interfaces
{
    public interface IMinoristaService
    {
    List<Minorista> GetAllMinorista();
    MinoristaResponse GetMinoristaById(int id);
    MinoristaResponse CreateMinorista(MinoristaRequest minorista);
    MinoristaResponse UpdateMinorista(int id, MinoristaRequest minorista);
    MinoristaResponse DeleteMinorista(int id);

    }
}
