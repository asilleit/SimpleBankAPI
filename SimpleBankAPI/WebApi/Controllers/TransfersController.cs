using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using System.Security.Authentication;

namespace SimpleBankAPI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class TransfersController : ControllerBase
    {
        private readonly postgresContext _context;
        private readonly ITransfersBusiness _transfersBusiness;
        private readonly IJwtAuth _jwtAuth;

        public TransfersController(postgresContext context, ITransfersBusiness transfersBusiness, IJwtAuth jwtAuth)
        {
            _context = context;
            _transfersBusiness = transfersBusiness;
            _jwtAuth = jwtAuth;
        }

        // POST: Transfers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostTransfer([FromBody] TransferRequest transfer)
        {
            try
            {

                int userId = int.Parse(_jwtAuth.GetClaim(authToken: Request.Headers.Authorization, claimName: "user"));
                var response = await _transfersBusiness.Create(transfer, userId);
                if (response is null)
                {
                    return BadRequest(StatusCodes.Status400BadRequest);
                }
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    ArgumentException => BadRequest(ex.Message),
                    AuthenticationException => Unauthorized(ex.Message),
                    _ => StatusCode(StatusCodes.Status400BadRequest, ex.Message)
                };
            }
        }
    }
}
