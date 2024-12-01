using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using Entity;
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
            return new string[] { "Shbat1", "Shalom" };
        }

        // GET api/<Users>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Users>
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
            var res = await service.Post(user);
            if (res != null)
                return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);
            return BadRequest();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> PostLogin([FromQuery] string email, string password)
        {
            var newUser = await service.PostLogin(email, password);
            if (newUser != null)
                return Ok(newUser);

            return NoContent();

        }

        [HttpPost]
        [Route("password")]
        public int PostPassword([FromQuery] string password)
        {
            return service.PostPassword(password);
        }

        // PUT api/<Users>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] User userToUpdate)
        {
            var res = await service.Put(id, userToUpdate);
            if (res != null)
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
