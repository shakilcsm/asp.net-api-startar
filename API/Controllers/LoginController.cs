using API.Framework.Entity;
using API.Framework.Repository;
using API.Framework.Shared;
using API.Framework.Shared.Extension;
using API.Requests.Login;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginRepository _repo;

        public LoginController(LoginRepository repo)
        {
            _repo = repo;
        }


        [HttpPost("password")]
        public ActionResult<LoginPayload> Password([FromBody] UsernameAndPasswordLoginRequest? request)
        {
            //! initial validation before checking the database
            if (request == null)
            {
                return BadRequest("No username and password provided.");
            }
            if (string.IsNullOrEmpty(request.Username))
            {
                return BadRequest("Username is required.");
            }
            if (string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Password is required.");
            }

            //! Check if the user exists in the database and validate the password
            try
            {
                LoginPayload profile = _repo.Password(request.Username, request.Password.ToMD5Hash());
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("social")]
        public ActionResult<LoginPayload> Password([FromBody] SocialLoginRequest? request)
        {
            //! initial validation before checking the database
            if (request == null)
            {
                return BadRequest("Invalid social login request.");
            }
            if (string.IsNullOrEmpty(request.Id))
            {
                return BadRequest("Social ID is required.");
            }
            if (string.IsNullOrEmpty(request.Provider))
            {
                return BadRequest("Provider is required.");
            }

            //! Check if the user exists in the database and validate the password
            try
            {
                LoginPayload profile = _repo.Social(request.Id, request.Provider);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
