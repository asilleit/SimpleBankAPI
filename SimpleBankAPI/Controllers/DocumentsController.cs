using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.JWT;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Response;
using System.Security.Authentication;

namespace SimpleBankAPI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class DocumentsController :ControllerBase
    {
        private readonly postgresContext _context;
        protected IDocumentsBusiness _documentsBusiness;
        protected IJwtAuth _jwtAuth;
        protected IAccountsBusiness _accountsBusiness;

        public DocumentsController(postgresContext context, IDocumentsBusiness documentsBusiness, IAccountsBusiness accountsBusiness, IJwtAuth jwtAuth)
        {
            _context = context;
            _documentsBusiness = documentsBusiness;
            _accountsBusiness = accountsBusiness;
            _jwtAuth = jwtAuth;
        }

        // GET: api/Documents
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocumentsByAccount()
        {
            //return await _context.Documents.ToListAsync();
            try
            {
                int userId = int.Parse(_jwtAuth.GetClaim(Request.Headers.Authorization, "user"));
                int id = 23;  // 
                                
                var documento = await _documentsBusiness.GetById(id);
                //Get Account ID
                if (documento.AccountId != userId) return StatusCode(StatusCodes.Status401Unauthorized, "User don't have Owner from document");


                var documents = await _documentsBusiness.GetDocumentsByAccount(userId);
                var accountResponseList = DocumentResponse.FromListDocumentAccount(documents);
                return StatusCode(StatusCodes.Status200OK, documents);
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
                        return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                }
            }
        }

        //// GET: api/Documents/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetDocument(int id)
        {
            try
            {
                int userId = int.Parse(_jwtAuth.GetClaim(Request.Headers.Authorization, "user"));
                var documento = await _documentsBusiness.GetById(id);
                var contaWithUser = await _accountsBusiness.GetById(documento.AccountId);
                //Get Account ID
                if (contaWithUser.UserId != userId) return StatusCode(StatusCodes.Status401Unauthorized, "User don't have Owner from document");

                var document = await _context.Documents.FindAsync(id);

                if (document == null)
                {
                    return NotFound();
                }

                return StatusCode(StatusCodes.Status200OK, document);
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
                        return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                }
            }
        }

        // POST: api/Documents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostDocument(IFormFile file, int id)
        {
            try
            {
                var account = await _accountsBusiness.GetById(id);
                int userId = int.Parse(_jwtAuth.GetClaim(Request.Headers.Authorization, "user"));
                var createdDocument = await _documentsBusiness.Create(file, account.Id, userId);
                return StatusCode(StatusCodes.Status201Created, createdDocument);

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
                        return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                }
            }

        }
        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
    }
}
