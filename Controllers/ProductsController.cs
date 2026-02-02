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

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _context.Products.FirstOrDefaultAsync(p => p.Id == id));
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            Product product = new Product()
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                CreatedAt = DateTime.Now,

            };
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
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto,int id)
        {
            var updatedProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            updatedProduct.Description= updateProductDto.Description;
            updatedProduct.Price= updateProductDto.Price;
            updatedProduct.Name=updateProductDto.Name;
            await _context.SaveChangesAsync();
            return Ok();

        }

    }
}
