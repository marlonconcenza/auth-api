using System;
using System.Threading.Tasks;
using auth_infra.Models;

namespace auth_infra.Interfaces
{
    public interface IAuthService
    {
        Task<Auth> authenticate(string email, string password);
    }
}
