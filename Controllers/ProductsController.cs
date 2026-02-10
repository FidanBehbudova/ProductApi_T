using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.DAL;
using ProductApi.DAL.Repositories.Abstract;
using ProductApi.Entities;
using ProductApi.Entities.Dtos.Products;
using System.Diagnostics.Eventing.Reader;

namespace ProductApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GetProductDto>> GetAllProducts()
        {
            var product = await _repository.GetAllAsync(includes:"Category");
            var dto = _mapper.Map<List<GetProductDto>>(product);
            return Ok(dto);
        }


        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _repository.GetAsync(c => c.Id == id, includes: "Category");
            var dto = _mapper.Map<GetProductDto>(product);

            return Ok(dto);

        }

        [HttpGet]
        public async Task<IActionResult> GetPaginate(int page,int size)
        {
            var products = await _repository.GetPaginateAsync(page, size, includes: "Category");
            var result = _mapper.Map<List<GetProductDto>>(products);
            return Ok(result);

        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            product.CreatedAt= DateTime.Now;
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deletedProduct = await _repository.GetAsync(p => p.Id == id);
            _repository.Remove(deletedProduct);
            await _repository.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto, int id)
        {
            var updatedProduct = await _repository.GetAsync(p => p.Id == id);
            _mapper.Map(updateProductDto, updatedProduct);
            await _repository.SaveChangesAsync();
            return Ok();

        }

    }
}
