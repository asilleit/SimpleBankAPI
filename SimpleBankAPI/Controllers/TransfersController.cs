using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBankAPI.Interfaces;
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

        public TransfersController(postgresContext context, ITransfersBusiness transfersBusiness)
        {
            _context = context;
            _transfersBusiness = transfersBusiness;
        }

        // POST: Transfers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostTransfer([FromHeader] int id, [FromBody] TransferRequest transfer)
        {
            try
            {
                var response = await _transfersBusiness.Create(transfer);
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
                    default:
                        return StatusCode(StatusCodes.Status400BadRequest, "Bad Request");
                }
            }
        }
        private bool TransferExists(int id)
        {
            return (_context.Transfers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
