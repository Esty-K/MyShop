using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        // GET: api/<Users>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Shbat", "Shalom" };
        }

        // GET api/<Users>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Users>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
           User u =service.Post(user);
            if (u==null)
                return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<User> PostLogin([FromQuery] string email, string password)
        {
            User newUser = service.PostLogin(email,password);
            if(newUser!=null)
                return Ok(newUser);
            
            return NoContent();

        }

        // PUT api/<Users>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] User userToUpdate)
        {
            service.Put(id, userToUpdate);
            if (userToUpdate != null)
                return Ok();

       
            return BadRequest();

        }

        // DELETE api/<Users>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
