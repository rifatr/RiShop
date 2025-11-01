using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(
        IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductBrand> productBrandsRepo,
        IGenericRepository<ProductType> productTypesRepo,
        IMapper mapper) : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository = productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository = productBrandsRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepository = productTypesRepo;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProducts(
            string sort, int? brandId, int? typeId)
        {
            var specifications = new ProductsWithTypesAndBrandsSpecification(sort, brandId, typeId);
            var products = await _productRepository.ListEntityWithSpecificationAsync(specifications);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var specifications = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepository.GetEntityWithSpecification(specifications);

            if(product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepository.GetAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepository.GetAllAsync());
        }
    }
}