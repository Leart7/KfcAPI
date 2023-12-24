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
    public class HomeCategoryController : ControllerBase
    {
        private readonly ICategoryRepository<HomeCategory> _homeCategoryRepository;
        private readonly IMapper _mapper;

        public HomeCategoryController(ICategoryRepository<HomeCategory> homeCategoryRepository, IMapper mapper)
        {
            _homeCategoryRepository = homeCategoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetHomeCategories()
        {
            var homeCategories = await _homeCategoryRepository.GetAllCategories();
            var homeCategoriesDto = _mapper.Map<List<HomeCategoryDto>>(homeCategories);

            return Ok(homeCategoriesDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateHomeCategory([FromBody] HomeCategoryRequestDto homeCategoryDto)
        {
            var homeCategoryDomainModel = _mapper.Map<HomeCategory>(homeCategoryDto);
            var newHomeCategory = await _homeCategoryRepository.CreateCategory(homeCategoryDomainModel);
            var returnedHomeCategory = _mapper.Map<HomeCategoryDto>(newHomeCategory);

            return Ok(returnedHomeCategory);
        }

        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateHomeCategory([FromRoute] int id, [FromBody] HomeCategoryRequestDto homeCategoryDto)
        {
            var homeCategoryDomainModel = _mapper.Map<HomeCategory>(homeCategoryDto);
            homeCategoryDomainModel = await _homeCategoryRepository.UpdateCategory(id, homeCategoryDomainModel);
            if (homeCategoryDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<HomeCategoryDto>(homeCategoryDomainModel));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteHomeCategory([FromRoute] int id)
        {
            var homeCategory = await _homeCategoryRepository.DeleteCategory(id);
            if (homeCategory == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<HomeCategoryDto>(homeCategory));
        }
    }
}
