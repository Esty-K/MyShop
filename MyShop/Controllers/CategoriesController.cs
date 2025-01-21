using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using Entity;
using AutoMapper;
using DTO;
using System.Collections.Generic;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService service;
        private readonly IMapper mapper;

        public CategoriesController(ICategoryService categoryservice , IMapper mapper)
        { 
            this.mapper = mapper;
            this.service = categoryservice;
           
        }

        // GET: api/<Categories>
        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get()
        {
            List<Category> categories = await service.Get();
            if (categories != null)
            {
                List<CategoryDTO> categoriesDTO = mapper.Map<List<Category>, List<CategoryDTO>>(categories);
                return Ok(categoriesDTO);
            }
            return BadRequest();
        }



    }
}
