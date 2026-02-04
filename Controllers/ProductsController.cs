using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.DAL;
using ProductApi.Entities;
using ProductApi.Entities.Dtos.Products;
using System.Diagnostics.Eventing.Reader;

namespace ProductApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GetProductDto>> GetAllProducts()
        {
            var result = await _context.Products.Include(p=>p.Category).ToListAsync();
           var product= _mapper.Map<List<GetProductDto>>(result);
            return Ok(product);
        }


        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return Ok(_mapper.Map<GetProductDto>(result));
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            product.CreatedAt= DateTime.Now;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deletedProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            _context.Products.Remove(deletedProduct);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto, int id)
        {
            var updatedProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            _mapper.Map(updateProductDto, updatedProduct);
            await _context.SaveChangesAsync();
            return Ok();

        }

    }
}
