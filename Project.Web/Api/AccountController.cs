using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Project.Domain.Context;
using Project.Domain.People;
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
            pass = EncriptPassword(login, pass);

            User user = _context.Users.FirstOrDefault(u => u.Login == login && u.Password == pass);

            if (user == null)
                return NotFound();

            UserModel userModel = _mapper.Map<UserModel>(user);
            userModel.Token = TokenService.GenerateToken(userModel);

            await Task.CompletedTask;

            return Ok(userModel);
        }

        [HttpPost, Route("logout")]
        public ActionResult Logout()
        {
            return Ok();
        }

        [HttpPost, Route("create"), AllowAnonymous]
        public ActionResult CreateAccount([FromBody] UserModel model)
        {
            var user = _mapper.Map<User>(model);

            Person person = new Person();
            _context.People.Add(person);
            _context.SaveChanges();

            user.PersonId = person.Id;

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut, Route("update")]
        [ProducesResponseType(typeof(UserModel), 200)]
        public ActionResult UpdateAccount([FromBody] UserModel model)
        {
            User user = _context.Users.Find(model.Id);

            if (user == null)
                return BadRequest();

            user.Login = model.Login;
            user.Email = model.Email;
            user.Person.Name = model.Person.Name;
            user.Person.Birth = model.Person.Birth;

            _context.SaveChanges();

            return Ok(_mapper.Map<UserModel>(user));
        }

        [HttpPut, Route("change-pass")]
        public ActionResult ChangePass(string login, string pass, string newPass)
        {
            pass = EncriptPassword(login, pass);

            var user = _context.Users.FirstOrDefault(u => u.Login == login && u.Password == pass);

            if (user == null)
                return NotFound();

            user.Password = newPass;
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet, Route("verify-auth")]
        public ActionResult VerifyAuth()
        {
            return Ok();
        }

        private static string EncriptPassword(string login, string pass)
        {
            byte[] salt = Encoding.UTF8.GetBytes(login);
            byte[] passByte = Encoding.UTF8.GetBytes(pass);
            byte[] sha256 = new SHA256Managed().ComputeHash(passByte.Concat(salt).ToArray());
            return Convert.ToBase64String(sha256);
        }
    }
}
