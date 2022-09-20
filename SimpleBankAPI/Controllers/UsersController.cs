﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;

namespace SimpleBankAPI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly postgresContext _context;
        protected IUserBusiness _userBusiness;

        public UsersController(postgresContext context, IUserBusiness userBusiness)
        {
            _context = context;
            _userBusiness = userBusiness;
        }



        // POST: v1/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost(Name="CreateUser")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                var createdUser = await _userBusiness.Create(request);

                return StatusCode(StatusCodes.Status201Created, request);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case ArgumentException: return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                    default:
                        return StatusCode(StatusCodes.Status400BadRequest, "Bad Request");
                }
            }
        }

        [HttpPost("login", Name = "Login")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest userRequest)
        {
            try
            {
                var user = await _userBusiness.Login(userRequest);

                var userResponse = LoginUserResponse.FromUserToLoginUserResponse(user);
                return StatusCode(StatusCodes.Status201Created, userResponse);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case ArgumentException: 
                        return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                    default: 
                        return StatusCode(StatusCodes.Status400BadRequest, "Bad request");
                };
            }
        }
        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
