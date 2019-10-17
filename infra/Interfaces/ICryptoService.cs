using System;

namespace auth_infra.Interfaces
{
    public interface ICryptoService
    {
        string Decrypt(string encryptedText);
        string Encrypt(string plainText);
    }
}
