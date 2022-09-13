using System;
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

        //// GET: Users
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        //{
        //  if (_context.Users == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.Users.ToListAsync();
        //}

        //// GET: Users/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<User>> GetUser(int id)
        //{
        //  if (_context.Users == null)
        //  {
        //      return NotFound();
        //  }
        //    var user = await _context.Users.FindAsync(id);

        //    if (user == null || !UserExists(id))
        //    {
        //        return NotFound();
        //    }


        //    return Ok(user.Id + " "+ 
        //        user.Username);
        //}



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

                //User user = CreateUserRequest.RequestToUser(request);
                //_context.Users.Add(user);
                //await _context.SaveChangesAsync();

                //return Ok(new CreateUserResponse
                //    {
                //        UserId = request.id,
                //        Username = request.UserName,
                //        Email = request.email,
                //        FullName = request.FullName
                //    });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Bad Request");
                //return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
                //return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                    case ArgumentException: return BadRequest(ex.Message);
                    default: return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                };
            }
        }


        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
