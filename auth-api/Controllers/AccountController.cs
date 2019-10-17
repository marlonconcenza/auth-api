using System;
using System.Threading.Tasks;
using auth_infra.Interfaces;
using auth_infra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace auth_api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IAccountService _accountService { get; set; }

        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> add([FromBody] Account account)
        {
            Response response = new Response();
            response.path = Request.Path.Value;
            response.success = false;

            try
            {
                if (account != null) {

                    await _accountService.add(account);

                    account.password = null;
                    response.success = true;
                    response.data = account;
                    response.id = account.id;

                    return Ok(response);  
                }

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getById(int id)
        {
            Response response = new Response();
            response.path = Request.Path.Value;
            response.success = false;

            try
            {
                var account = await _accountService.getById(id);

                if (account == null) {
                    response.message = "Account not found";
                    return NotFound(response);
                }

                // only allow admins to access other account records
                var currentAccountId = int.Parse(User.Identity.Name);
                if (id != currentAccountId && !User.IsInRole(Role.Admin)) {
                    return Forbid();
                }

                response.data = account;
                response.id = account.id;
                response.success = true;

                return Ok(response);
            } 
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return BadRequest(response);            
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("{init}/{offset}")]
        public async Task<IActionResult> get(int init, int offset)
        {
            if (init < 0 || offset <= 0) {
                return BadRequest();
            }

            Response response = new Response();
            response.path = Request.Path.Value;
            response.success = false;

            try
            {
                var accounts = await _accountService.getAll(init, offset);

                response.data = accounts;
                response.success = true;

                return Ok(response);
            } 
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return BadRequest(response);            
        }
    }
}
