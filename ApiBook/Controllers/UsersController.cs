using LatihanJWT.Data;
using LatihanJWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LatihanJWT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        DBText entitas;
        public UsersController(DBText db)
        {
            this.entitas = db;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get()
        {
            List<UsersDto> users = new List<UsersDto>();
            var data = entitas.users.Include(s=>s.roles).ToList();
            if (data.Count>0)
            {
                foreach (var item in data)
                {
                    var view = new UsersDto
                    {
                        id = item.id,
                        password_user = item.password_user,
                        status_user = item.status_user,
                        birthdate = item.birthdate,
                        email = item.email,
                        name_user = item.name_user,
                        roles_id = item.roles_id,
                        roles = new RoleDto
                        {
                            id = item.roles.id,
                            title = item.roles.title
                        }
                    };
                    users.Add(view);
                }
                
                return Ok(users);
            }
            if (data.Count==0)
            {
                return NotFound();
            }
            return BadRequest();
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
            public RoleDto roles { get; set; }
        }
        public class UsersDtoAdd
        {
            public string roles_id { get; set; }
            public string email { get; set; }
            public string password_user { get; set; }
            public string name_user { get; set; }
            public DateTime birthdate { get; set; }
        }
        public class RoleDto
        {
            public string id { get; set; }
            public string title { get; set; }
        }
        public class UsersStatus
        {
            public string status_user { get; set; }
        }
        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsersDtoAdd value)
        {
            var check = entitas.users.ToList();
            var id = "USR-"+(check.Count+1).ToString();
            var data = new Users
            {
                id = id,
                birthdate = value.birthdate,
                name_user = value.name_user,
                email = value.email,
                password_user = value.password_user,
                roles_id = value.roles_id,
                status_user = "Active"
            };
            entitas.users.Add(data);
            if (entitas.SaveChanges()>0)
            {
                return Ok(data);
            }
            if (entitas.SaveChanges()==0)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UsersStatus value)
        {
            var data = entitas.users.Where(s=>s.id == id).FirstOrDefault();
            if (data!=null)
            {
                data.status_user = value.status_user;
            }
            if (data==null)
            {
                return NotFound();
            }
            if (entitas.SaveChanges()>0)
            {
                return Ok(data);
            }
            if (entitas.SaveChanges()==0)
            {
                return BadRequest();
            }
            return BadRequest();
        }
    }
}
