using Microsoft.AspNetCore.Mvc;
using Entity;
using Services;
using AutoMapper;
using DTO;
using System.Collections.Generic;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {


        private readonly IProductService service;
        private readonly IMapper mapper;

        public ProductsController(IProductService service, IMapper mapper)
        {
            this.mapper = mapper;
            this.service = service;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get([FromQuery] string? searchName, [FromQuery]int? minPrice, [FromQuery]  int? maxPrice, [FromQuery]  int?[] categoryIds)
        {
            List < Product > products = await service.Get(searchName, minPrice, maxPrice,categoryIds);
            if (products != null)
              {
                List<ProductDTO> productsDTO = mapper.Map<List<Product>, List<ProductDTO>>(products);
                return Ok(productsDTO);
            }
            return BadRequest();
        }



    }
}
