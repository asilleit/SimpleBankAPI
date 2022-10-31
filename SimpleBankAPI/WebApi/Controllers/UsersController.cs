using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserBusiness _userBusiness;
        private readonly IJwtAuth _jwtAuth;
        private readonly ITokenBusiness _ITokenBusiness;

        public UsersController(postgresContext context, IUserBusiness userBusiness, IJwtAuth jwtAuth, ITokenBusiness tokenBusiness)
        {
            _context = context;
            _userBusiness = userBusiness;
            _jwtAuth = jwtAuth;
            _ITokenBusiness = tokenBusiness;
        }


        // POST: v1/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost(Name = "CreateUser")]
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

        //private bool UserExists(int id)
        //{
        //    return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
