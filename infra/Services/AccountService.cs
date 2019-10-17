using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using auth_infra.Data;
using auth_infra.Interfaces;
using auth_infra.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace auth_infra.Services
{
    public class AccountService : IAccountService
    {
        private ICryptoService _cryptoService { get; }
        private AccountContext _context { get; }

        public AccountService(AccountContext context, ICryptoService cryptoService)
        {
            this._context = context;
            this._cryptoService = cryptoService;
        }

        public async Task<Account> add(Account account)
        {
            account.password = this._cryptoService.Encrypt(account.password);
            account.createdAt = DateTime.Now;

            var exists = await this.getByEmail(account.email);

            if (exists != null) {
                throw new Exception("Account already exists");
            }
            
            this._context.Accounts.Add(account);
            await this._context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> getByEmail(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(
                    x => x.email == email);       
        }

        public async Task<IEnumerable<Account>> getAll(int init, int offset)
        {
            return await _context.Accounts
                            .OrderBy(x => x.id)
                            .Skip(init)
                            .Take(offset)
                            .ToListAsync();
        }

        public async Task<Account> getById(int id)
        {
            return await _context.Accounts.Where(x => x.id == id).FirstOrDefaultAsync();
        }

        public async Task<Account> getAccount(string email, string password)
        {
            return await _context.Accounts.FirstOrDefaultAsync(
                    x => x.email == email &&
                    x.password == password);
        }
    }
}