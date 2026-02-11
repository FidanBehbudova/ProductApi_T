using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.DAL;
using ProductApi.DAL.Repositories.Abstract;
using ProductApi.Entities;
using ProductApi.Entities.Dtos.Categories;

namespace ProductApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork work, IMapper mapper)
        {
            _work = work;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GetCategoryDto>> GetAllCategories()
        {
           return Ok(await _work.Products.GetAllAsync());
        }


        [HttpGet]
        public async Task<ActionResult<GetCategoryDto>> GetAllCategoriesPeginated(int page,int size)
        {
            return Ok(await _work.Products.GetPaginateAsync(page,size));
        }


        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
           return Ok(await _work.Categories.GetAsync(c=>c.Id == id));

        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {

           var category =_mapper.Map<Category>(createCategoryDto);
            await _work.Categories.AddAsync(category);
            await _work.SaveChangesAsync();
            return Ok(category);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryt(int id)
        {
            var deletedCategory = await _work.Categories.GetAsync(c => c.Id == id);
            _work.Categories.Remove(deletedCategory);
            await _work.SaveChangesAsync();
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto, int id)
        {
            var updatedCategory = await _work.Categories.GetAsync(c => c.Id == id);
            _mapper.Map(updateCategoryDto, updatedCategory);
            await _work.SaveChangesAsync();
            return Ok();
        }


    }
}
