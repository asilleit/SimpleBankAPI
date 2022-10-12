﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using NuGet.Common;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.JWT;
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
        protected IJwtAuth _jwtAuth;

        public UsersController(postgresContext context, IUserBusiness userBusiness, IJwtAuth jwtAuth)
        {
            _context = context;
            _userBusiness = userBusiness;
            _jwtAuth = jwtAuth;
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
            catch (InvalidCastException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {

            return StatusCode(StatusCodes.Status401Unauthorized, "Bad Request passou +ersonal");

            }
        }

        [HttpPost("login", Name = "Login")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest userRequest)
        {
            try
            {
                var user = await _userBusiness.Login(userRequest);

                JwtSecurityToken token = _jwtAuth.CreateJwtToken(user);

                var userResponse = LoginUserResponse.FromUserToLoginUserResponse(user, token);
                return StatusCode(StatusCodes.Status201Created, userResponse);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case ArgumentException: 
                        return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                   case InvalidCastException:
                        return StatusCode(StatusCodes.Status404NotFound, ex.Message);
                    default: 
                        return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                };
            }
        }

        //[HttpPost("revalidate", Name = "revalidate")]
        //[Produces("application/json")]
        //[ProducesResponseType(typeof(RevalidateResponse), StatusCodes.Status201Created)]
        //[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> RevalidateUser([FromBody] string authorization)
        //{
        //    try
        //    {
        //        //var token = _jwtAuth.GetClaim(userRequest, "user");


        //        int userId = int.Parse(_jwtAuth.GetClaim(Request.Headers.Authorization, "user"));

        //        //var result = await _userBusiness.Revalidate(authorization);
        //        if (result == null)
        //            return StatusCode(StatusCodes.Status404NotFound, "User not found");


        //        var validateResponse = RevalidateResponse.CreateRevalidateResponse(authorization);
        //        return StatusCode(StatusCodes.Status200OK, validateResponse);
        //    }
        //    catch (Exception ex)
        //    {
        //        switch (ex)
        //        {
        //            case ArgumentException:
        //                return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
        //            case InvalidCastException:
        //                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        //            default:
        //                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        //        };
        //    }
        //}

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
