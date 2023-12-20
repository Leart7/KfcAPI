using AutoMapper;
using KfcApi.CustomActionFilters;
using KfcApi.DTOs;
using KfcApi.Models;
using KfcApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KfcApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuAddOnController : ControllerBase
    {
        private readonly IAddOnRepository<MenuAddOn> _menuAddOnRepository;
        private readonly IMapper _mapper;

        public MenuAddOnController(IAddOnRepository<MenuAddOn> menuAddOnRepository, IMapper mapper)
        {
            _menuAddOnRepository = menuAddOnRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenuAddOns()
        {
            var menuAddOns = await _menuAddOnRepository.GetAllAddOns();

            return Ok(_mapper.Map<List<MenuAddOnDto>>(menuAddOns));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateMenuAddOn([FromBody] MenuAddOnRequestDto menuAddOnDto)
        {
            var menuAddOnDomainModel = _mapper.Map<MenuAddOn>(menuAddOnDto);
            var newMenuAddOn = await _menuAddOnRepository.CreateAddOn(menuAddOnDomainModel);

            return Ok(_mapper.Map<MenuAddOnDto>(newMenuAddOn));
        }

        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateMenuAddOn([FromRoute] int id, [FromBody] MenuAddOnRequestDto menuAddOnDto)
        {
            var menuAddOnDomainModel = _mapper.Map<MenuAddOn>(menuAddOnDto);
            menuAddOnDomainModel = await _menuAddOnRepository.UpdateAddOn(id, menuAddOnDomainModel);

            if (menuAddOnDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MenuAddOnDto>(menuAddOnDomainModel));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteMenuAddOn([FromRoute] int id)
        {
            var menuAddOn = await _menuAddOnRepository.DeleteAddOn(id);
            if (menuAddOn == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MenuAddOnDto>(menuAddOn));
        }
    }
}
