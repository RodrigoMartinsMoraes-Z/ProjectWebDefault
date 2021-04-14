using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Project.Domain.Context;
using Project.Domain.Users;
using Project.Web.Models.Users;

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Project.Web.Api
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public AccountController(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost, Route("login"), AllowAnonymous]
        [ProducesResponseType(typeof(UserModel), 200)]
        public async Task<ActionResult> Logar(string login, string pass)
        {
            pass = EncriptarSenha(login, pass);

            User user = _context.Users.FirstOrDefault(u => u.Login == login && u.Password == pass);

            _loggedUser = user;

            if (user == null)
                return NotFound();

            UserModel usuarioModel = _mapper.Map<UserModel>(user);
            usuarioModel.Token = TokenService.GenerateToken(usuarioModel);

            await Task.CompletedTask;

            return Ok(usuarioModel);
        }

        private static string EncriptarSenha(string login, string senha)
        {
            byte[] salt = Encoding.UTF8.GetBytes(login);
            byte[] senhaByte = Encoding.UTF8.GetBytes(senha);
            byte[] sha256 = new SHA256Managed().ComputeHash(senhaByte.Concat(salt).ToArray());
            return Convert.ToBase64String(sha256);
        }
    }
}
