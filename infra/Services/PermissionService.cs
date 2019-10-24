using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using auth_infra.Data;
using auth_infra.Interfaces;
using auth_infra.Models;
using Microsoft.EntityFrameworkCore;

namespace auth_infra.Services
{
    public class PermissionService : IPermissionService
    {
        private DataBaseContext _context { get; }

        public PermissionService(DataBaseContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Permission>> getByAccount(int account)
        {
            return await _context.Permissions.Where(x => x.accountId == account).ToListAsync();
        }

        public async Task<Permission> create(Permission permission)
        {
             _context.Permissions.Add(permission);
             await _context.SaveChangesAsync();
             return permission;
        }
    }
}
