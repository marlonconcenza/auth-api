using System;
using System.Threading.Tasks;
using auth_infra.Interfaces;
using auth_infra.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace auth_infra.Services
{
    public class AuthService : IAuthService
    {
        private IAccountService _accountService { get; }
        private ICryptoService _cryptoService { get; }
        private ITokenService _tokenService { get; }

        public AuthService(IAccountService accountService, ICryptoService cryptoService, ITokenService tokenService)
        {
            this._accountService = accountService;
            this._cryptoService = cryptoService;
            this._tokenService = tokenService;
        }
        public async Task<Auth> authenticate(string email, string password)
        {
            Auth _auth = null;

            try {

                password = this._cryptoService.Encrypt(password);

                var _account = await _accountService.getAccount(email, password);

                if (_account != null) {

                    _auth = new Auth();
                    _auth.id = _account.id;
                    _auth.email = email;
                    _auth.token = this._tokenService.createToken(_account);
                }

            } catch(Exception ex) {
                throw ex;
            }

            return _auth;
        }
    }
}
