using JwtProject.Constants;
using JwtProject.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            this._config = config;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var currentUser = this.GetCurrentUser();
            return Ok($"Hola {currentUser.FirstName}, tu eres {currentUser.Rol}");            
        }

        [HttpPost]
        public ActionResult Login(LoginUser userLogin)
        {
            var user = this.Authenticate(userLogin);
            if (user != null)
            {
                //TODO: Crea el token
                var token = this.Generate(user);
                return Ok(token);
            }
            return NotFound("Usuario no encontrado");
        }

        private UserModel Authenticate(LoginUser userLogin)
        {
            var currentUser = UserConstants.Users.FirstOrDefault(user => user.UserName.ToLower() == userLogin.UserName.ToLower() 
            && user.Password.ToLower() == userLogin.Password.ToLower());

            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }

        private string Generate(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._config["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //TODO: Crea los claims (o reclamaciones)
            var claims = new[] 
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Rol),
                //TODO: Se pueden crear claims personalizados.
                new Claim("idEmpresa", "840926"),
            };
            //TODO: Crea el token
            var token = new JwtSecurityToken(
                    //TODO: "Jwt:Issuer" Es quien está generando el token.
                    this._config["Jwt:Issuer"],
                    this._config["Jwt:Audience"],
                    claims,
                    //TODO: Tiempo de expiración del token.
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials:credentials
                );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var  userClaims = identity.Claims;
                return new UserModel
                {
                    UserName = userClaims.FirstOrDefault(x=> x.Type == ClaimTypes.NameIdentifier)?.Value,
                    EmailAddress = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                    FirstName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value,
                    LastName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value,
                    Rol = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value                    
                };
            }
            return null;
        }
    }
}
