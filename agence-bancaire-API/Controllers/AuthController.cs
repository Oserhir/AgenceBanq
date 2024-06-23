using agence_bancaire_API.CustomActionFilters;
using agence_bancaire_API.DTO;
using agence_bancaire_API.Global_Classes;
using agence_bancaire_API.Repositories;
using agence_bancaire_Business_Layer;
using agence_bancaire_Business_Layer.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace agence_bancaire_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
       private readonly ItokenReposirtoy _token;
        public AuthController(ItokenReposirtoy itoken)
        {
            this._token = itoken;
        }

        [HttpPost]
        [Route("Register")]
        // [ValidateModule]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            clsUser _User = new clsUser();

            _User.PersonID = request.PersonID;
            _User.Password = request.Password;

            try
            {
                if (_User.Save())
                {
                    return CreatedAtAction(nameof(Register), null);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, 
                                $"Failed to Register User. Internal server error occurred.");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ex}");
            }

        }


        [HttpPost]
        [Route("Login")]
        [ValidateModule]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            clsUser _User = clsUser.FindByEmailAndPasswordAsync(request.Email, request.Password);

            if (_User != null)
            {
                // Create Token
                var Jwt = _token.CreateJWTToken(_User, "Admin");

                return Ok(new { JWTToken = Jwt });
            }
            else
            {
                return BadRequest("Email or Password is incorrect");
            }

        }
    }
}
