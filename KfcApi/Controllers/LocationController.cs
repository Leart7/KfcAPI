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
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationController(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLocations()
        {
            var locations = await _locationRepository.GetAllLocations();
            var locationsDto = _mapper.Map<List<LocationDto>>(locations);

            return Ok(locationsDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateLocation([FromBody] LocationRequestDto locationDto)
        {
            var locationDomainModel = _mapper.Map<Location>(locationDto);
            
            var newLocation = await _locationRepository.CreateLocation(locationDomainModel);
            var returnedLocation = _mapper.Map<LocationDto>(newLocation);

            return Ok(returnedLocation);
        }

        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateLocation([FromRoute] int id, [FromBody] LocationRequestDto locationDto)
        {
            var locationDomainModel = _mapper.Map<Location>(locationDto);
            locationDomainModel = await _locationRepository.UpdateLocation(id, locationDomainModel);
            if (locationDomainModel == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<LocationDto>(locationDomainModel));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteLocation([FromRoute] int id)
        {
            var location = await _locationRepository.DeleteLocation(id);
            if(location == null)
            {
                return NotFound();
            }
            var deletedProduct = _mapper.Map<LocationDto>(location);
            return Ok(deletedProduct);
        }
    }
}
