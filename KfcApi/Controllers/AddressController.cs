using AutoMapper;
using KfcApi.DTOs;
using KfcApi.Models;
using KfcApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KfcApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressController(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetAddresses([FromRoute] string userId)
        {
            var addresses =  await _addressRepository.GetAllAddresses(userId);
            return Ok(_mapper.Map<List<AddressDto>>(addresses));
        }

        //[HttpGet]
        //[Route("{id}")]
        //public async Task<IActionResult> GetAddress([FromRoute] int id)
        //{
        //    var address = await _addressRepository.GetAddress(id);
        //    if(address == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(_mapper.Map<AddressDto>(address));
        //}

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] AddressRequestDto addressRequestDto)
        {
            var addressDomainModel = _mapper.Map<Address>(addressRequestDto);
            var newAddress = await _addressRepository.CreateAddress(addressDomainModel);
            return Ok(_mapper.Map<AddressDto>(newAddress));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAddress([FromRoute] int id, [FromBody] AddressUpdateRequestDto addressRequestDto)
        {
            var addressDomainModel = _mapper.Map<Address>(addressRequestDto);
            if(addressDomainModel == null)
            {
                return NotFound();
            }

            var updatedAddress = await _addressRepository.UpdateAddress(id, addressDomainModel);
            return Ok(_mapper.Map<AddressDto>(updatedAddress));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] int id)
        {
            var address = await _addressRepository.DeleteAddress(id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AddressDto>(address));
        }
    }
}
