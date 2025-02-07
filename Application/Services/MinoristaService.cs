using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MinoristaService : IMinoristaService
    {
        // private readonly IMinoristaRepository _minoristaRepository;
        public List<Minorista> GetAllMinorista()
        {
            return new List<Minorista>
            {
                new () 
                {
                    Id=1,
                    FirstName="Facundo",
                    LastName="Solari",
                    NameAccount="facu",
                    Password="facu123",
                    Email="facu@hotmail.com",
                    Dni=12323456,
                    PhoneNumber="+543413500300",
                    Address="Santafe 1234",
                },
                new ()
                {
                    Id=2,
                    FirstName="Cristian",
                    LastName="Ninotti",
                    NameAccount="cris",
                    Password="cris123",
                    Email="cris@hotmail.com",
                    Dni=34732713,
                    PhoneNumber="+543415155611",
                    Address="San Lorenzo 3624",
                }
            };
        }
    }
}
