using AutoMapper;
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
        private readonly IMapper _mapper;


        public CategoriesController(AppDbContext context, IMapper mapper)
        {
            _context = context;  
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GetCategoryDto>> GetAllCategories()
        {
            var result = await _context.Categories.ToListAsync();
            _mapper.Map<List<GetCategoryDto>>(result);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return Ok(_mapper.Map<GetCategoryDto>(result));

        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {

            await _context.Categories.AddAsync(_mapper.Map<Category>(createCategoryDto));
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



        //[HttpPut]
        //public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto, int id)
        //{
        //    var updatedCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        //    _context.Categories.Update(_mapper.Map<Category>(updateCategoryDto));
        //    await _context.SaveChangesAsync();
        //    return Ok();

        //}


        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto, int id)
        {
            var updatedCategory = await _context.Categories.FirstAsync(c => c.Id == id);

            _mapper.Map(updateCategoryDto, updatedCategory);

            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}
