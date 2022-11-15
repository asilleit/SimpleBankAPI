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
        private readonly IUserBusiness _userBusiness;
        private readonly IJwtAuth _jwtAuth;
        private readonly ITokenBusiness _ITokenBusiness;

        public LoginController(postgresContext context, IUserBusiness userBusiness, IJwtAuth jwtAuth, ITokenBusiness iTokenBusiness)
        {
            _context = context;
            _userBusiness = userBusiness;
            _jwtAuth = jwtAuth;
            _ITokenBusiness = iTokenBusiness;
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
                return ex switch
                {
                    ArgumentException => BadRequest(ex.Message),
                    InvalidCastException => Unauthorized(ex.Message),
                    _ => StatusCode(StatusCodes.Status400BadRequest, ex.Message)
                };
            }
        }


        [HttpPost("revalidate", Name = "Revalidate")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]

        public async Task<IActionResult> RevalidateUser([FromBody] RevalidateRequest revalid)
        {

            int userId = int.Parse(_jwtAuth.GetClaim(authToken: Request.Headers.Authorization, claimName: "user"));

            var response = await _ITokenBusiness.Revalidate(userId, revalid);

            var userResponse = new LoginUserResponse(response.Item1, response.Item2, response.Item3, response.Item4, response.Item5);
            return StatusCode(StatusCodes.Status201Created, userResponse);

        }

    }
}
