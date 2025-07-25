using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public required string PictureUrl { get; set; }
        public int ProductBrandId { get; set; }
        public required ProductBrand ProductBrand { get; set; }
        public int ProductTypeId { get; set; }
        public required ProductType ProductType { get; set; }
    }

    public class ProductBrand : BaseEntity
    {
        public required string Name { get; set; }
    }

    public class ProductType : BaseEntity
    {
        public required string Name { get; set; }
    }
}