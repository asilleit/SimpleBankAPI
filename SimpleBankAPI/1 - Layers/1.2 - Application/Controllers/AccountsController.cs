using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;
using System.Security.Authentication;

namespace SimpleBankAPI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly postgresContext _context;
        private readonly IAccountsBusiness _accountsBusiness;
        private readonly IJwtAuth _jwtAuth;
        private readonly IDocumentsBusiness _documentsBusiness;
        private readonly IDocumentsRepository _documentsRepository;
        public AccountsController(postgresContext context, IAccountsBusiness accountsBusiness, IDocumentsRepository documentsRepository, IJwtAuth jwtAuth, IDocumentsBusiness documentsBusiness)
        {
            _context = context;
            _accountsBusiness = accountsBusiness;
            _jwtAuth = jwtAuth;
            _documentsBusiness = documentsBusiness;
            _documentsRepository = documentsRepository;
        }

        // GET: api/Accounts
        [HttpGet(Name = "GetAccounts")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<AccountResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            try
            {
                //int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                int userId = int.Parse(_jwtAuth.GetClaim(authToken: Request.Headers.Authorization, claimName: "user"));

                var accounts = await _accountsBusiness.GetAccountsByUser(userId);
                List<AccountResponse> accountResponseList = AccountResponse.FromListAccountsUser(accounts);
                return StatusCode(StatusCodes.Status200OK, accountResponseList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost(Name = "CreateAccount")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostAccount([FromBody] AccountRequest request)
        {
            try
            {
                int userId = int.Parse(_jwtAuth.GetClaim(authToken: Request.Headers.Authorization, claimName: "user"));

                var createdUser = await _accountsBusiness.Create(request, userId);

                return StatusCode(StatusCodes.Status201Created, request);
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    ArgumentException => BadRequest(ex.Message),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, ex.Message)
                };
            }
        }

        // GET: Accounts/5
        [HttpGet("{id:int}", Name = "GetAccount")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                int userId = int.Parse(_jwtAuth.GetClaim(authToken: Request.Headers.Authorization, claimName: "user"));
                var account = await _accountsBusiness.GetById(id);
                //Get Account ID
                if (account.UserId != userId) return StatusCode(StatusCodes.Status401Unauthorized, "User don't have Owner from account");

                var accountResponse = AccountResponse.ToAcountResponse(account);
                return StatusCode(StatusCodes.Status200OK, accountResponse);
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    ArgumentException => BadRequest(ex.Message),
                    AuthenticationException => Unauthorized(ex.Message),
                    _ => StatusCode(StatusCodes.Status400BadRequest, ex.Message)
                };
            }
        }
        // GET: Accounts/5/Documents
        [HttpGet("{id:int}/doc", Name = "GetDoccumentByAccount")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocumentsByAccount([FromRoute] int id)
        {
            try
            {
                int userId = int.Parse(_jwtAuth.GetClaim(Request.Headers.Authorization, "user"));

                var account = await _accountsBusiness.GetById(id);
                //Get Document ID
                if (account.UserId != userId) return StatusCode(StatusCodes.Status401Unauthorized, "User don't have Owner from document");


                var documents = await _documentsBusiness.GetDocumentsByAccount(id);
                var accountResponseList = DocumentResponse.FromListDocumentAccount(documents);
                return StatusCode(StatusCodes.Status200OK, documents);
            }

            catch (Exception ex)
            {
                return ex switch
                {
                    ArgumentException => BadRequest(ex.Message),
                    AuthenticationException => Unauthorized(ex.Message),
                    _ => StatusCode(StatusCodes.Status400BadRequest, ex.Message)
                };
            }
        }

        ////// GET: api/Documents/5
        //[HttpGet("{id}")]
        //[Produces("application/json")]
        //[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        ////[Authorize]
        //public async Task<IActionResult> GetDocument(int id)
        //{
        //    try
        //    {
        //        int userId = int.Parse(_jwtAuth.GetClaim(Request.Headers.Authorization, "user"));
        //        var documento = await _documentsBusiness.GetById(id);
        //        var contaWithUser = await _accountsBusiness.GetById(documento.AccountId);
        //        //Get Account ID
        //        if (contaWithUser.UserId != userId) return StatusCode(StatusCodes.Status401Unauthorized, "User don't have Owner from document");

        //        var document = await _context.Documents.FindAsync(id);

        //        if (document == null)
        //        {
        //            return NotFound();
        //        }

        //        return StatusCode(StatusCodes.Status200OK, document);
        //    }
        //    catch (Exception ex)
        //    {
        //        switch (ex)
        //        {
        //            case AuthenticationException:
        //                return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
        //            case ArgumentException:
        //                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        //            default:
        //                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        //        }
        //    }
        //}

        // POST: api/Documents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id:int}/doc", Name = "UploadDocument")]
        [RequestSizeLimit(2 * 1024 * 1024)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostDocument([FromRoute] int id)
        {
            try
            {
                var content = HttpContext.Request.Form.Files[0];
                var account = await _accountsBusiness.GetById(id);
                int userId = int.Parse(_jwtAuth.GetClaim(authToken: Request.Headers.Authorization, claimName: "user"));
                var createdDocument = await _documentsBusiness.Create(content, account.Id, userId);
                return StatusCode(StatusCodes.Status201Created, createdDocument);
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    AuthenticationException => Unauthorized(ex.Message),
                    ArgumentException => BadRequest(ex.Message),
                    _ => StatusCode(StatusCodes.Status400BadRequest, ex.Message)
                };
            }
        }


        [HttpGet("{id:int}/doc/{docid}", Name = "DownloadDocument")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DownloadDocument([FromRoute] int id, int docId)
        {
            try
            {
                int userId = int.Parse(_jwtAuth.GetClaim(authToken: Request.Headers.Authorization, claimName: "user"));

                var account = await _accountsBusiness.GetById(id);

                var result = await _documentsBusiness.DownloadDocument(docId);


                MemoryStream ms = new MemoryStream(result.File);
                return new FileStreamResult(ms, "application/pdf");

                var stream = new MemoryStream();
                await stream.WriteAsync(result.File, 0, result.File.Length);
                stream.Position = 0;
                return File(stream, result.FileType, result.FileName);

            }
            catch (Exception ex)
            {
                return ex switch
                {
                    AuthenticationException => Unauthorized(ex.Message),
                    ArgumentException => BadRequest(ex.Message),
                    _ => StatusCode(StatusCodes.Status400BadRequest, ex.Message)
                };
            }
        }
    }
}