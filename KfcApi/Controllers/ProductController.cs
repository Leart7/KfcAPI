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
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetAllProducts();
            var productsDto = _mapper.Map<List<ProductDto>>(products);
            return Ok(productsDto);
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            var product = await _productRepository.GetProduct(id);
            if(product == null)
            {
                return NotFound();
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateProduct([FromForm] ProductRequestDto productDto)
        {
            var productDomainModel = _mapper.Map<Product>(productDto);
            var newProduct = await _productRepository.CreateProduct(productDomainModel);

            var returnedProduct = _mapper.Map<ProductDto>(newProduct);
            return Ok(returnedProduct);
        }

        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateProduct(int id, ProductRequestDto productDto)
        {
            var productDomainModel = _mapper.Map<Product>(productDto);
            productDomainModel = await _productRepository.UpdateProduct(id, productDomainModel);
            if(productDomainModel == null)
            {
                return NotFound();
            }

            var updatedProductDto = _mapper.Map<ProductDto>(productDomainModel);
            return Ok(updatedProductDto);

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var product = await _productRepository.DeleteProduct(id);
            if(product == null)
            {
                return NotFound();
            }

            var deletedProduct = _mapper.Map<ProductDto>(product);
            return Ok(deletedProduct);
        }
    }
}
