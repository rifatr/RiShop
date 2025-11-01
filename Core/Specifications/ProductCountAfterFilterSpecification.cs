using Core.Entities;

namespace Core.Specifications
{
    public class ProductCountAfterFilterSpecification : BaseSpecifications<Product>
    {
        public ProductCountAfterFilterSpecification(ProductSpecParams productParams)
            : base (x =>
                (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                (!productParams.TypeId.HasValue  || x.ProductTypeId  == productParams.TypeId)
            )
        {
        }
    }
}