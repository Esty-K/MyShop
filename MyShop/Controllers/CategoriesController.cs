using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using Services;
using Entity;
using AutoMapper;
using DTO;
using System.Collections.Generic;
using MyShop;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService service;
        private readonly IMapper mapper;
        private readonly IMemoryCache cache;

        public CategoriesController(ICategoryService categoryservice, IMapper mapper, IMemoryCache cache)
        {
            this.mapper = mapper;
            this.service = categoryservice;
            this.cache = cache;
        }

        // GET: api/<Categories>
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> Get()
        {


            if (!cache.TryGetValue("categoriesCache", out List<Category> categories))
            {
                categories = await service.Get();
                cache.Set("categoriesCache", categories, TimeSpan.FromMinutes(30));
            }

            List<CategoryDTO> categoriesDTO = mapper.Map<List<Category>, List<CategoryDTO>>(categories);
            return Ok(categoriesDTO);

        }

    }
}
