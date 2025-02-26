using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class MinoristaRepository : IMinoristaRepository
    {
        private readonly EcommerceDbContext _minorista;

        public MinoristaRepository(EcommerceDbContext minorista)
        {
            _minorista = minorista;
        }

        public List<Minorista> GetAllMinoristas()
        {
            return _minorista.Minoristas.ToList();
        }

        public Minorista? GetMinoristaById(int id)
        {
            return _minorista.Minoristas.FirstOrDefault(m => m.Id == id);
        }

        public void CreateMinorista (Minorista minorista)
        {
            _minorista.Minoristas.Add(minorista);
            _minorista.SaveChanges();
        }

        public void UpdateMinorista (Minorista minorista)
        {
            _minorista.Minoristas.Update(minorista);
            _minorista.SaveChanges();
        }

        public void DeleteMinorista (Minorista minorista)
        {
            _minorista.Minoristas.Remove(minorista);
            _minorista.SaveChanges();
        }
    }
}
