using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using Entity;
using AutoMapper;
using DTO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService service;
        private readonly IMapper mapper;
        private readonly ILogger<UsersController> logger;

   public UsersController(IUserService service, IMapper mapper, ILogger<UsersController> logger)
        {
            this.mapper = mapper;
            this.service = service;
            this.logger = logger;
        }

        // GET api/<Users>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            var user = await service.GetById(id);
            if (user != null)
            {
                UserDTO userDTO = mapper.Map<User, UserDTO>(user);
                return Ok(userDTO); 
            } return NoContent();
        }


        // POST api/<Users>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PostUserDTO userDTO)
        {
            User user = mapper.Map<PostUserDTO, User>(userDTO);
            var newUser = await service.Post(user);
            if (newUser != null)
            {
                UserDTO userdto = mapper.Map<User, UserDTO>(newUser);
                return CreatedAtAction(nameof(GetById), new { id = newUser.UserId }, userdto);
            }
            return BadRequest("The password is too weak");
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> PostLogin([FromQuery] string email, string password)
        {
            var newUser = await service.PostLogin(email, password);
            if (newUser != null)
            {
                UserDTO userdto = mapper.Map<User, UserDTO>(newUser);
                logger.LogInformation($"Login attempted with User Name,{email},and password {password}");
                return Ok(userdto);
            }
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
        public async Task<ActionResult> Put(int id, [FromBody] PostUserDTO user)
        {
            User userToUpdate = mapper.Map<PostUserDTO, User>(user);
            var updateUser = await service.Put(id,userToUpdate);
            if (updateUser != null)
            {
                return Ok("The password is too weak");
            }
            return BadRequest();
        }
    }
}
