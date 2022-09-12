using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public TransfersController(postgresContext context)
        {
            _context = context;
        }

        //// GET: Transfers
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Transfer>>> GetTransfers()
        //{
        //  if (_context.Transfers == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.Transfers.ToListAsync();
        //}

        

        // POST: Transfers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transfer>> PostTransfer(int id, Transfer transfer)
        {
            if (id != transfer.Id)
            {
                return BadRequest();
            }

            _context.Entry(transfer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransferExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool TransferExists(int id)
        {
            return (_context.Transfers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
