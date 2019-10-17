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
    }
}
