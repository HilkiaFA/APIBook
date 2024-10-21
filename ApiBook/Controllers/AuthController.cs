using LatihanJWT.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LatihanJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        DBText entitas;
        IConfiguration configuration;
        public AuthController(DBText dB, IConfiguration configuration)
        {
            this.entitas = dB;
            this.configuration = configuration;
        }
        public class Auth
        {
            public string email { get; set; }
            public string password { get; set; }
        }

        public class UsersDto
        {
            public string id { get; set; }
            public string roles_id { get; set; }
            public string email { get; set; }
            public string password_user { get; set; }
            public string name_user { get; set; }
            public DateTime birthdate { get; set; }
            public string status_user { get; set; }
            public string token { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Auth value)
        {
            var data = entitas.users.Where(s => s.email == value.email && s.password_user == value.password).FirstOrDefault();
            if (data != null)
            {
                Class.iduser = data.id;
                Class.idrole = data.roles_id;
                string token = loadtoken(data.name_user);

                var tampil = new UsersDto
                {
                    birthdate = data.birthdate,
                    email = data.email,
                    id = data.id,
                    name_user = data.name_user,
                    password_user = data.password_user,
                    roles_id = data.roles_id,
                    status_user = data.status_user,
                    token = token
                };
                return Ok(tampil);
            }
            return BadRequest();
        }

        private string loadtoken(string name_user)
        {
            var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var credential = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, name_user),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: claims,
                notBefore:DateTime.Now,
                expires:DateTime.Now.AddMinutes(10),
                signingCredentials: credential
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
