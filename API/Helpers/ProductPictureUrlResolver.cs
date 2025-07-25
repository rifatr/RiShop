using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class ProductPictureUrlResolver(IConfiguration config) : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _config = config;

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(_config["BackendBaseUrl"]))
            {
                if (!string.IsNullOrEmpty(source.PictureUrl))
                {
                    return _config["BackendBaseUrl"] + "/" + source.PictureUrl;
                }
                else
                {
                    throw new InvalidOperationException($"Picture URL does not exist for product id: {source.Id}.");
                }
            }
            else
            {
                throw new InvalidOperationException("BackendBaseUrl is not configured.");
            }
        }
    }
}