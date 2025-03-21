﻿using Domain.Interfaces;
using Infraestructure.Context;
using Domain.Entities;
using Infraestructure.Migrations;
using Application.Services;

namespace Infraestructure.Data
{
    public class MayoristaRepository : IMayoristaRepository
    {
        private readonly EcommerceDbContext _mayorista;



        public MayoristaRepository(EcommerceDbContext mayorista)
        {
            _mayorista = mayorista;
        }

        public List<Mayorista> GetAllMayoristas()
        {
            return _mayorista.Mayoristas.ToList();
        }
        public Mayorista? GetMayoristaById(int id)
        {
            return _mayorista.Mayoristas.FirstOrDefault(m => m.Id == id);
        }

        public void CreateMayorista(Mayorista mayorista)
        {

            _mayorista.Mayoristas.Add(mayorista);
            _mayorista.SaveChanges();
        }

        public void UpdateMayorista(Mayorista mayorista)
        {
            _mayorista.Mayoristas.Update(mayorista);
            _mayorista.SaveChanges();
        }

        //SoftDelete
        public void SoftDeleteMayorista(Mayorista mayorista)
        {
            mayorista.Available = false;
            _mayorista.SaveChanges();
        }

        //Hard Delete
        public void DeleteMayorista(Mayorista mayorista)
        {
            _mayorista.Mayoristas.Remove(mayorista);
            _mayorista.SaveChanges();
        }


    }
}
