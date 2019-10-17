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
    public class AcountService : IAcountService
    {
        private readonly ICryptoService _cryptoService;
        private readonly AcountContext _context;

        public AcountService(AcountContext context, ICryptoService cryptoService)
        {
            this._context = context;
            this._cryptoService = cryptoService;
        }

        public async Task<Acount> add(Acount acount)
        {
            acount.password = this._cryptoService.Encrypt(acount.password);
            acount.createdAt = DateTime.Now;

            var exists = await this.getByEmail(acount.email);

            if (exists != null) {
                throw new Exception("Acount already exists");
            }
            
            this._context.Acounts.Add(acount);
            await this._context.SaveChangesAsync();
            return acount;
        }

        public async Task<Acount> getByEmail(string email)
        {
            return await _context.Acounts.FirstOrDefaultAsync(
                    x => x.email == email);       
        }

        public async Task<IEnumerable<Acount>> getAll()
        {
            return await _context.Acounts.ToListAsync();
        }

        public async Task<Acount> getById(int id)
        {
            return await _context.Acounts.Where(x => x.id == id).FirstOrDefaultAsync();
        }
    }
}