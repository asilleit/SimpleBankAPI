using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.JWT;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;

namespace SimpleBankAPI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class TransfersController : ControllerBase
    {
        private readonly postgresContext _context;
        protected ITransfersBusiness _transfersBusiness;
        protected IJwtAuth _jwtAuth;

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
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostTransfer([FromHeader] string Authorization, [FromBody] TransferRequest transfer)
        {
            try
            {

                int userId = int.Parse(_jwtAuth.GetClaim(Authorization, "user"));
                var response = await _transfersBusiness.Create(transfer, userId);
                if (response is null)
                {
                    return BadRequest(StatusCodes.Status400BadRequest);
                }
                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case  ArgumentException:
                        return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                    case AuthenticationException:
                        return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
                    default:
                        return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
                }
            }
        }
        private bool TransferExists(int id)
        {
            return (_context.Transfers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
