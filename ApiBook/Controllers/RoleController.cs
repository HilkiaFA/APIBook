using LatihanJWT.Data;
using LatihanJWT.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LatihanJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        DBText entitas;
        public RoleController(DBText dB)
        {
            this.entitas = dB;
        }
        public class Role
        {
            public string id { get; set; }
            public string title { get; set; }
        }
        [HttpGet]
        public IActionResult Get()
        {
            List<Role> list = new List<Role>();
            var data = entitas.roles.ToList();
            foreach (var item in data)
            {
                var role = new Role
                {
                    id = item.id,
                    title = item.title
                };
                list.Add(role);
            }
            return Ok(list);
        }
    }
}
