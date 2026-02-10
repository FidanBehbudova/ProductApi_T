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

        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GetCategoryDto>> GetAllCategories()
        {
           return Ok(await _repository.GetAllAsync());
        }


        [HttpGet]
        public async Task<ActionResult<GetCategoryDto>> GetAllCategoriesPeginated(int page,int size)
        {
            return Ok(await _repository.GetPaginateAsync(page,size));
        }


        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
           return Ok(await _repository.GetAsync(c=>c.Id == id));

        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {

           var category =_mapper.Map<Category>(createCategoryDto);
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
            return Ok(category);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryt(int id)
        {
            var deletedCategory = await _repository.GetAsync(c => c.Id == id);
            _repository.Remove(deletedCategory);
            await _repository.SaveChangesAsync();
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto, int id)
        {
            var updatedCategory = await _repository.GetAsync(c => c.Id == id);
            _mapper.Map(updateCategoryDto, updatedCategory);
            await _repository.SaveChangesAsync();
            return Ok();
        }


    }
}
