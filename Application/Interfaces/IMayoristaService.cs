using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMayoristaService
    {
        List<Mayorista> GetAllMayoristas();
        MayoristaResponse GetMayoristaById(int id);
        MayoristaResponse CreateMayorista(MayoristaRequest request);
        MayoristaResponse UpdateMayorista(int id, MayoristaRequest request);
        MayoristaResponse DeleteMayorista(int id);
    }
}
