using Core.Entities;

namespace Core.Interfaces
{
    public interface ICartRepository
    {
        Task<CustomerCart> GetCartAsync(string cartId);
        Task<CustomerCart> CreateOrUpdateCartAsync(CustomerCart customerCart);
        Task<bool> DeleteCartAsync(string cartId);
    }
}