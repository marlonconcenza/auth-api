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
        private IAcountService _acountService;
        private ICryptoService _cryptoService;
        private ITokenService _tokenService;

        public AuthService(IConfiguration configuration, 
                IAcountService acountService, 
                ICryptoService cryptoService,
                ITokenService tokenService)
        {
            this._acountService = acountService;
            this._cryptoService = cryptoService;
            this._tokenService = tokenService;
        }
        public async Task<Auth> authenticate(string email, string password)
        {
            Auth _auth = null;

            try {

                password = this._cryptoService.Encrypt(password);

                var _acount = await _acountService.getAcount(email, password);

                if (_acount != null) {

                    _auth = new Auth();
                    _auth.id = _acount.id;
                    _auth.email = email;
                    _auth.token = this._tokenService.createToken(_acount);
                }

            } catch(Exception ex) {
                throw ex;
            }

            return _auth;
        }
    }
}
