using LatihanJWT.Data;
using LatihanJWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LatihanJWT.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BooksController : ControllerBase
    {
        DBText dbtext;
        public BooksController(DBText dBText)
        {
            this.dbtext = dBText;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string? id)
        {
            if (Class.idrole != "ROL02")
            {
                return BadRequest();
            }
            if (!string.IsNullOrEmpty(id))
            {
                var data = dbtext.books.Where(s => s.id == id).FirstOrDefault();
                if (data!=null)
                {
                    return Ok(data);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                var data = dbtext.books.ToList();
                return Ok(data);
            }
         }

        public class BooksDto
        {
            public string book_title { get; set; }
            public string author { get; set; }
            public string publisher { get; set; }
            public int stock { get; set; }
            public int price { get; set; }
            public string id_user { get; set; }
        }
        public class BooksEdit
        {
            public string book_title { get; set; }
            public string author { get; set; }
            public string publisher { get; set; }
            public int stock { get; set; }
            public int price { get; set; }
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BooksDto value)
        {
            if (Class.idrole != "ROL02")
            {
                return BadRequest();
            }
            var check = dbtext.books.ToList();
            var id = "BK"+check.Count+1;
            var data = new Books
            {
                id = id,
                author = value.author,
                book_title = value.book_title,
                id_user = value.id_user,
                publisher = value.publisher,
                stock = value.stock,
                price = value.price
            };
            dbtext.books.Add(data);
            if (dbtext.SaveChanges()>0)
            {
                return Ok(data);
            }
            if (dbtext.SaveChanges()==0)
            {
                return BadRequest();
            }
            return BadRequest();
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] BooksEdit value)
        {
            if (Class.idrole!= "ROL02")
            {
                return BadRequest();
            }
            var data = dbtext.books.Where(s => s.id == id).FirstOrDefault();
            if (data!=null)
            {
                if (value.book_title!="string")
                {
                    data.book_title = value.book_title;
                }
                if (value.publisher!="string")
                {
                    data.publisher = value.publisher;
                }
                if (value.stock!=0)
                {
                    data.stock = value.stock;
                }
                if (value.stock!=0)
                {
                    data.price = value.price;
                }
                if (value.author!="string")
                {
                    data.author = value.author;
                }
                data.id_user=Class.iduser;
            }
            if (dbtext.SaveChanges()>0)
            {
                return Ok(data);
            }
            return NotFound();
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var data = dbtext.books.Where(s => s.id == id).FirstOrDefault();
            dbtext.books.Remove(data);
            if (dbtext.SaveChanges()>0)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
