using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using auth_infra.Models;

namespace auth_infra.Interfaces
{
    public interface IPermissionService
    {
        Task<Permission> create(Permission permission);
        Task<IEnumerable<Permission>> getByAccount(int account);
    }
}
