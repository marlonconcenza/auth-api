using System;
using auth_infra.Models;

namespace auth_infra.Interfaces
{
    public interface ITokenService
    {
        string createToken(Account account);
    }
}
