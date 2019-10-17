using System;
using System.Threading.Tasks;
using auth_infra.Interfaces;
using auth_infra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace auth_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthService _authService { get; set; }
        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> authenticate([FromBody]Auth authParam)
        {
            Response response = new Response();
            response.path = Request.Path.Value;
            response.success = false;

            try
            {
                var auth = await _authService.authenticate(authParam.email, authParam.password);
    
                if (auth != null) {

                    response.success = true;
                    response.data = auth;
                    response.id = auth.id;

                    return Ok(response);
                }

                response.message = "Email or password is incorrect";

            } catch (Exception ex) {
                response.message = ex.Message;
            }

            return BadRequest(response);
        }
    }
}
