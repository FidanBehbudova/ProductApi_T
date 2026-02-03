using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.DAL;
using ProductApi.Entities;
using ProductApi.Entities.Dtos.Categories;

namespace ProductApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await _context.Categories.ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _context.Categories.FirstOrDefaultAsync(c => c.Id == id));
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            Category category = new Category()
            {
                Name = createCategoryDto.Name,

            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryt(int id)
        {
            var deletedCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            _context.Categories.Remove(deletedCategory);
            await _context.SaveChangesAsync();
            return Ok();
        }



        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto, int id)
        {
            var updatedCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            updatedCategory.Name = updateCategoryDto.Name;
            await _context.SaveChangesAsync();
            return Ok();

        }

    }
}
