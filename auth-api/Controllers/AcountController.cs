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
    public class AcountController : ControllerBase
    {
        public IAcountService _acountService { get; set; }

        public AcountController(IAcountService acountService)
        {
            this._acountService = acountService;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> add([FromBody] Acount acount)
        {
            Response response = new Response();
            response.path = Request.Path.Value;
            response.success = false;

            try
            {
                if (acount != null) {

                    await _acountService.add(acount);

                    acount.password = null;
                    response.success = true;
                    response.data = acount;
                    response.id = acount.id;

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
                var acount = await _acountService.getById(id);

                if (acount == null) {
                    response.message = "Acount not found";
                    return NotFound(response);
                }

                // only allow admins to access other acount records
                var currentAcountId = int.Parse(User.Identity.Name);
                if (id != currentAcountId && !User.IsInRole(Role.Admin)) {
                    return Forbid();
                }

                response.data = acount;
                response.id = acount.id;
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
                var acounts = await _acountService.getAll(init, offset);

                response.data = acounts;
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
