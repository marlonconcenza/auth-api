using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using auth_infra.Models;

namespace auth_infra.Interfaces
{
    public interface IAcountService
    {
        Task<Acount> add(Acount acount);
        Task<IEnumerable<Acount>> getAll(int init, int offset);
        Task<Acount> getById(int id);
        Task<Acount> getByEmail(string email);
        Task<Acount> getAcount(string email, string password);
    }
}
