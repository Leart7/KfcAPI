using AutoMapper;
using KfcApi.CustomActionFilters;
using KfcApi.DTOs;
using KfcApi.Models;
using KfcApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KfcApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetAllCategories();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);

            return Ok(categoriesDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequestDto categoryDto)
        {
            var categoryDomainModel = _mapper.Map<Category>(categoryDto);
            var newCategory = await _categoryRepository.CreateCategory(categoryDomainModel);
            var returnedCategory = _mapper.Map<CategoryDto>(newCategory);

            return Ok(returnedCategory);
        }

        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] CategoryRequestDto categoryDto)
        {
            var categoryDomainModel = _mapper.Map<Category>(categoryDto);
            categoryDomainModel = await _categoryRepository.UpdateCategory(id, categoryDomainModel);
            if(categoryDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CategoryDto>(categoryDomainModel));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var category = await _categoryRepository.DeleteCategory(id);
            if(category == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CategoryDto>(category));
        }
    }
}
