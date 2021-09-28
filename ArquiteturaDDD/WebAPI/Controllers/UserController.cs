using Application.Interfaces;
using Entities.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Token;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _iuserApplication;

        private readonly UserManager<AplicationUser> _userManager;

        private readonly SignInManager<AplicationUser> _signInManager;

        public UserController(IUserApplication iuserApplication, UserManager<AplicationUser> userManager, SignInManager<AplicationUser> signInManager)
        {
            _iuserApplication = iuserApplication;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Criação do Token De acesso
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Criação do token</returns>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        [AllowAnonymous]
        [Produces("applicatio/json")]
        [HttpPost("/api/CreateToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateToken([FromBody] Login login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password)) return Unauthorized();

            var IsValidUser = await _iuserApplication.ThereIsUser(login.Email, login.Password);

            if (IsValidUser) return TokenJwt();

            else return Unauthorized();
        }

        /// <summary>
        /// Adicionar usuário. 
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Usuário Criado</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response code="401">Unauthorized</response>
        [AllowAnonymous]
        [Produces("applicatio/json")]
        [HttpPost("/api/AddUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddUser([FromBody] Login login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password)) return Unauthorized();

            var user = await _iuserApplication.AddUser(login.Email, login.Password, login.Age, login.Cellphone);

            if (user) return Ok();

            else return BadRequest("Erro ao adicionar o usuário! ");
        }

        /// <summary>
        /// Criação do token de indentidaded
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response code="401">Unauthorized</response>
        [AllowAnonymous]
        [Produces("applicatio/json")]
        [HttpPost("/api/CreateTokenIdentity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateTokenIdentity([FromBody] Login login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password)) return Unauthorized();

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

            if (result.Succeeded) return TokenJwt();

            else return BadRequest("Erro ao criar o token");
        }
        /// <summary>
        /// Criação do token para o usuário
        /// </summary>
        /// <returns>Retonar o token criado</returns>
        private IActionResult TokenJwt()
        {
            var token = new TokenJwtBuilder()
                        .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                        .AddSubject("Empresa - Marias Fi-Fi")
                        .AddIssuer("Teste.Securiy.Bearer")
                        .AddAudience("Teste.Securiy.Bearer")
                        .AddClaim("UsuarioAPINumero", "1")
                        .AddExpiry(30)
                        .Builder();

            return Ok(token.value);
        }
    }
}
