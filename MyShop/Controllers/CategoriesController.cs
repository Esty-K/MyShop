using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory; // Add this line
using System.Text.Json;
using Services;
using Entity;
using AutoMapper;
using DTO;
using System.Collections.Generic;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService service;
        private readonly IMapper mapper;
        private readonly IMemoryCache cache; // Add this line
        private const string CacheKey = "categoriesCache"; // Add this line

        public CategoriesController(ICategoryService categoryservice, IMapper mapper, IMemoryCache cache) // Modify constructor
        {
            this.mapper = mapper;
            this.service = categoryservice;
            this.cache = cache; // Add this line
        }

        // GET: api/<Categories>
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> Get() // Modify return type
        {
            if (!cache.TryGetValue(CacheKey, out List<CategoryDTO> categoriesDTO)) // Add caching logic
            {
                List<Category> categories = await service.Get();
                if (categories != null)
                {
                    categoriesDTO = mapper.Map<List<Category>, List<CategoryDTO>>(categories);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(30)); // Set cache duration
                    cache.Set(CacheKey, categoriesDTO, cacheEntryOptions);
                }
                else
                {
                    return BadRequest();
                }
            }
            return Ok(categoriesDTO);
        }
    }
}
