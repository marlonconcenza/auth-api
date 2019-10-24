using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using auth_infra.Models;

namespace auth_infra.Interfaces
{
    public interface IAccountService
    {
        Task<Account> create(Account account);
        Task<IEnumerable<Account>> getAll(int init, int offset);
        Task<Account> getById(int id);
        Task<Account> getByEmail(string email);
        Task<Account> getAccount(string email, string password);
    }
}
