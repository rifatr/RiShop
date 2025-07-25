using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandsRepo, IGenericRepository<ProductType> productTypesRepo) : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepository = productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository = productBrandsRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepository = productTypesRepo;

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts()
        {
            var specifications = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productRepository.ListEntityWithSpecificationAsync(specifications);

            return products
                .Select(product =>
                    new ProductDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        PictureUrl = product.PictureUrl,
                        ProductBrand = product.ProductBrand.Name,
                        ProductType = product.ProductType.Name
                    })
                .ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var specifications = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepository.GetEntityWithSpecification(specifications);

            return new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    PictureUrl = product.PictureUrl,
                    ProductBrand = product.ProductBrand.Name,
                    ProductType = product.ProductType.Name
                };
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