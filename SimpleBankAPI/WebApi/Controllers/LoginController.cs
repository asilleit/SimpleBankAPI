using Microsoft.AspNetCore.Mvc;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;

namespace SimpleBankAPI.WebApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly postgresContext _context;
        protected IUserBusiness _userBusiness;
        protected IJwtAuth _JwtAuth;

        public LoginController(postgresContext context, IUserBusiness userBusiness, IJwtAuth jwtAuth)
        {
            _context = context;
            _userBusiness = userBusiness;
            _JwtAuth = jwtAuth;
        }

        [HttpPost("login", Name = "Login")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest userRequest)
        {
            try
            {
                var response = await _userBusiness.Login(userRequest);


                var userResponse = new LoginUserResponse(response.Item1, response.Item2, response.Item3, response.Item4, response.Item5);
                return StatusCode(StatusCodes.Status200OK, userResponse);
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

    }
}
