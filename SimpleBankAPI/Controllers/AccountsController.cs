﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBankAPI.Business;
using SimpleBankAPI.Data;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.JWT;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;

namespace SimpleBankAPI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly postgresContext _context;
        protected IAccountsBusiness _accountsBusiness;
        protected IJwtAuth _jwtAuth;
        public AccountsController(postgresContext context, IAccountsBusiness accountsBusiness, IJwtAuth jwtAuth)
        {
            _context = context;
            _accountsBusiness = accountsBusiness;
            _jwtAuth = jwtAuth;
        }

        // GET: api/Accounts
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts([FromHeader] string token)
        {
            try
            {
                //int userId = int.Parse(_jwtAuth.GetClaim(token));
                int userId = 2;
                var accounts = await _accountsBusiness.GetAccountsByUser(userId);
                var accountResponseList = AccountResponse.FromListAccountsUser(accounts);
                return StatusCode(StatusCodes.Status201Created, accountResponseList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAccount([FromBody] AccountRequest request, [FromHeader] string authorization)
        {
            try
            {
                int userId = 1;
                    //int.Parse(_jwtAuth.GetClaim(authorization));

                var createdUser = await _accountsBusiness.Create(request, userId);

                return StatusCode(StatusCodes.Status201Created, request);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case ArgumentException: return BadRequest(ex.Message);
                    default: return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                };
            }
        }

        // GET: Accounts/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                //Get Account ID
                var account = await _accountsBusiness.GetById(id);

                var accountResponse = AccountResponse.ToAcountResponse(account);
                return StatusCode(StatusCodes.Status201Created, accountResponse);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case AuthenticationException:
                        return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
                    case ArgumentException:
                        return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                    default:
                        return StatusCode(StatusCodes.Status400BadRequest, "Bad request");
                }
            }
        }
        private bool AccountExists(int id)
        {
            return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
