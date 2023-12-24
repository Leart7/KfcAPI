using AutoMapper;
using KfcApi.CustomActionFilters;
using KfcApi.DTOs;
using KfcApi.Models;
using KfcApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KfcApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User")] 
    public class AddOnController : ControllerBase
    {
        private readonly IAddOnRepository<AddOn> _addOnRepository;
        private readonly IMapper _mapper;

        public AddOnController(IAddOnRepository<AddOn> addOnRepository, IMapper mapper)
        {
            _addOnRepository = addOnRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAddOns()
        {
            var addOns = await _addOnRepository.GetAllAddOns();
            return Ok(_mapper.Map<List<AddOnDto>>(addOns));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public async Task<IActionResult> CreateAddOn([FromBody] AddOnRequestDto addOnDto)
        {
            var addOnDomainModel = _mapper.Map<AddOn>(addOnDto);
            var newAddOn = await _addOnRepository.CreateAddOn(addOnDomainModel);
            return Ok(_mapper.Map<AddOnDto>(newAddOn));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public async Task<IActionResult> UpdateAddOn([FromRoute] int id, [FromBody] AddOnRequestDto addOnDto)
        {
            var addOnDomainModel = _mapper.Map<AddOn>(addOnDto);
            addOnDomainModel = await _addOnRepository.UpdateAddOn(id, addOnDomainModel);

            if (addOnDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AddOnDto>(addOnDomainModel));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAddOn([FromRoute] int id)
        {
            var addOn = await _addOnRepository.DeleteAddOn(id);

            if (addOn == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AddOnDto>(addOn));
        }
    }
}
