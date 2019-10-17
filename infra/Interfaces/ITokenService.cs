using System;

namespace auth_infra.Interfaces
{
    public interface ITokenService
    {
        string createToken(string data);
    }
}
