using Application.Interfaces;
using Entities.Entites;
using Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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
        [Produces("application/json")]
        [HttpPost("/api/CreateToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateToken([FromBody] Login login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password)) return Unauthorized();

            var IsValidUser = await _iuserApplication.ThereIsUser(login.Email, login.Password);

            if (IsValidUser)
            {
                var userId = await _iuserApplication.UserIdReturn(login.Email);

                return Ok(TokenJwt(userId));
            }

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
        [Produces("application/json")]
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
        [Produces("application/json")]
        [HttpPost("/api/CreateTokenIdentity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateTokenIdentity([FromBody] Login login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password)) return Unauthorized();

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var userId = await _iuserApplication.UserIdReturn(login.Email);

                return Ok(TokenJwt(userId));
            }
            else return BadRequest("Erro ao criar o token");

        }

        /// <summary>
        /// Adicionar identidade do usuário
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response code="401">Unauthorized</response>
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AddUserIdentity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddUserIdentity([FromBody] Login login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password)) return Unauthorized();

            var parametrs = new AplicationUser
            {
                Age = login.Age,
                Email = login.Email,
                Cellphone = login.Cellphone,
                Type = UserType.Common,
                UserName = login.Email.Remove(5)
            };

            var result = await _userManager.CreateAsync(parametrs, login.Password);

            if (result.Errors.Any()) return BadRequest(string.Concat("Erro ao adicionar o usuário. ", result.Errors.ToList()[0].Description));

            //Geração de confirmação do e-mail. 
            var userId = await _userManager.GetUserIdAsync(parametrs);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(parametrs);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            //Retorno de confirmação do e-mail
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultEmail = await _userManager.ConfirmEmailAsync(parametrs, code);

            if (resultEmail.Succeeded) return Ok(string.Concat("Usuario ",parametrs.UserName, " Adicionado Com Sucesso! "));


            else return BadRequest(resultEmail.ToString());
        }

        /// <summary>
        /// Criação do token para o usuário
        /// </summary>
        /// <returns>Retonar o token criado</returns>
        private IActionResult TokenJwt([Optional] string userId)
        {
            var token = new TokenJwtBuilder()
                        .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                        .AddSubject("Empresa - Marias Fi-Fi")
                        .AddIssuer("Teste.Securiy.Bearer")
                        .AddAudience("Teste.Securiy.Bearer")
                        .AddClaim("userId", userId != string.Empty ? userId : "1")
                        .AddExpiry(30)
                        .Builder();

            return Ok(token.value);
        }
    }
}
